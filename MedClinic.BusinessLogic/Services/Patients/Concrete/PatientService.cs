using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Patients;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing patients.
/// </summary>
public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PatientService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPatientDto> _addPatientValidator;
    private readonly IValidator<UpdatePatientDto> _updatePatientValidator;
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addPatientValidator">The validator for adding a patient.</param>
    /// <param name="updatePatientValidator">The validator for updating a patient.</param>
    /// <param name="userService">The user service.</param>
    public PatientService(
        IUnitOfWork unitOfWork,
        ILogger<PatientService> logger,
        IMapper mapper,
        IValidator<AddPatientDto> addPatientValidator,
        IValidator<UpdatePatientDto> updatePatientValidator,
        IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addPatientValidator = addPatientValidator;
        _updatePatientValidator = updatePatientValidator;
        _userService = userService;
    }

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    /// <param name="patient">The patient DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created patient.</returns>
    public async Task<(bool, Guid)> CreatePatient(AddPatientDto patient)
    {
        try
        {
            // Validate the patient DTO
            var validationResult = _addPatientValidator.Validate(patient);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Start a transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Create a AddUserDto from the Patient information
                var userDto = new AddUserDto
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    BirthDate = patient.BirthDate
                };

                // Create the user using the UserService
                var (userCreated, userId) = await _userService.CreateUser(userDto);
                if (!userCreated)
                {
                    _logger.LogError("User creation failed for Patient creation.");
                    await _unitOfWork.RollbackTransactionAsync();
                    return (false, Guid.Empty);
                }

                // Map DTO to Patient entity
                var newPatient = _mapper.Map<Patient>(patient);
                newPatient.Id = Guid.NewGuid();
                newPatient.UserId = userId; // Link to the newly created User
                newPatient.CreatedAt = DateTime.UtcNow;

                // Add patient to the repository
                var isPatientCreated = await _unitOfWork.PatientWrite.AddAsync(newPatient);
                if (!isPatientCreated)
                {
                    _logger.LogError("Patient creation failed for {Id}.", newPatient.Id);
                    await _unitOfWork.RollbackTransactionAsync();
                    return (false, Guid.Empty);
                }

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Patient {PatientId} created successfully with User {UserId}.", newPatient.Id, userId);
                return (true, newPatient.Id);
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any error
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Transaction failed while creating patient.");
                return (false, Guid.Empty);
            }
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Patient creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the patient.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Deletes a patient by ID.
    /// </summary>
    /// <param name="id">The patient ID.</param>
    public async Task DeletePatient(Guid id)
    {
        try
        {
            var patient = await _unitOfWork.PatientRead.GetByIdAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID {PatientId} not found.", id);
                throw new KeyNotFoundException($"Patient with ID {id} not found.");
            }

            _unitOfWork.PatientWrite.Delete(patient);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Patient {PatientId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting patient with ID {PatientId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all patients with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of patient DTOs.</returns>
    public PagedResult<PatientDto> GetAllPatients(PatientSortFilterOptions options)
    {
        var patients = _unitOfWork.PatientRead.GetAll().AsNoTracking();
        var patientDtos = patients.Select(p => _mapper.Map<PatientDto>(p)).AsPagedResult(options);
        return patientDtos;
    }

    /// <summary>
    /// Updates an existing patient.
    /// </summary>
    /// <param name="patient">The patient DTO.</param>
    public async Task UpdatePatient(UpdatePatientDto patient)
    {
        try
        {
            var validationResult = _updatePatientValidator.Validate(patient);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Start a transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var existingPatient = await _unitOfWork.PatientRead.GetByIdAsync(patient.Id);
                if (existingPatient == null)
                {
                    _logger.LogWarning("Patient with ID {PatientId} not found.", patient.Id);
                    throw new KeyNotFoundException($"Patient with ID {patient.Id} not found.");
                }

                // Update related user information if necessary
                var userDto = new UpdateUserDto
                {
                    Id = existingPatient.UserId,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    BirthDate = patient.BirthDate
                };

                await _userService.UpdateUser(userDto);

                // Map updated properties from DTO to entity
                _mapper.Map(patient, existingPatient);

                var isUpdated = _unitOfWork.PatientWrite.Update(existingPatient);

                if (!isUpdated)
                {
                    _logger.LogError("Patient update failed for {PatientId}.", patient.Id);
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception($"Patient update failed for {patient.Id}");
                }

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Patient {PatientId} updated successfully.", patient.Id);
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any error
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Transaction failed while updating patient.");
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating patient {PatientId}.", patient.Id);
            throw;
        }
    }

    /// <summary>
    /// Gets a patient by ID.
    /// </summary>
    /// <param name="id">The patient ID.</param>
    /// <returns>The patient DTO.</returns>
    public async Task<PatientDto> GetPatientById(Guid id)
    {
        var patient = await _unitOfWork.PatientRead.GetByIdAsync(id);
        if (patient == null)
        {
            _logger.LogWarning("Patient with ID {PatientId} not found.", id);
            throw new KeyNotFoundException($"Patient with ID {id} not found.");
        }

        return _mapper.Map<PatientDto>(patient);
    }
}