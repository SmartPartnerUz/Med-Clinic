using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing doctors.
/// </summary>
public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DoctorService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddDoctorDto> _addDoctorValidator;
    private readonly IValidator<UpdateDoctorDto> _updateDoctorValidator;
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoctorService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addDoctorValidator">The validator for adding a doctor.</param>
    /// <param name="updateDoctorValidator">The validator for updating a doctor.</param>
    /// <param name="userService">The user service.</param>
    public DoctorService(
        IUnitOfWork unitOfWork,
        ILogger<DoctorService> logger,
        IMapper mapper,
        IValidator<AddDoctorDto> addDoctorValidator,
        IValidator<UpdateDoctorDto> updateDoctorValidator,
        IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addDoctorValidator = addDoctorValidator;
        _updateDoctorValidator = updateDoctorValidator;
        _userService = userService;
    }

    /// <summary>
    /// Creates a new doctor.
    /// </summary>
    /// <param name="doctor">The doctor DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created doctor.</returns>
    public async Task<(bool, Guid)> CreateDoctor(AddDoctorDto doctor)
    {
        try
        {
            // Validate the doctor DTO
            var validationResult = _addDoctorValidator.Validate(doctor);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Start a transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Create a AddUserDto from the Doctor information
                var userDto = new AddUserDto
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    PhoneNumber = doctor.PhoneNumber,
                    BirthDate = doctor.BirthDate
                };

                // Create the user using the UserService
                var (userCreated, userId) = await _userService.CreateUser(userDto);
                if (!userCreated)
                {
                    _logger.LogError("User creation failed for Doctor creation.");
                    await _unitOfWork.RollbackTransactionAsync();
                    return (false, Guid.Empty);
                }

                // Map DTO to Doctor entity
                var newDoctor = _mapper.Map<Doctor>(doctor);
                newDoctor.Id = Guid.NewGuid();
                newDoctor.UserId = userId; // Link to the newly created User
                newDoctor.CreatedAt = DateTime.UtcNow;
                newDoctor.UpdatedAt = DateTime.UtcNow;

                // Add doctor to the repository
                var isDoctorCreated = await _unitOfWork.DoctorWrite.AddAsync(newDoctor);
                if (!isDoctorCreated)
                {
                    _logger.LogError("Doctor creation failed for {Id}.", newDoctor.Id);
                    await _unitOfWork.RollbackTransactionAsync();
                    return (false, Guid.Empty);
                }

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Doctor {DoctorId} created successfully with User {UserId}.", newDoctor.Id, userId);
                return (true, newDoctor.Id);
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any error
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Transaction failed while creating doctor.");
                return (false, Guid.Empty);
            }
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Doctor creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the doctor.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Deletes a doctor by ID.
    /// </summary>
    /// <param name="id">The doctor ID.</param>
    public async void DeleteDoctor(Guid id)
    {
        try
        {
            var doctor = await _unitOfWork.DoctorRead.GetByIdAsync(id);
            if (doctor == null)
            {
                _logger.LogWarning("Doctor with ID {DoctorId} not found.", id);
                throw new KeyNotFoundException($"Doctor with ID {id} not found.");
            }

            _unitOfWork.DoctorWrite.Delete(doctor);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Doctor {DoctorId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting doctor with ID {DoctorId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all doctors with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of doctor DTOs.</returns>
    public PagedResult<DoctorDto> GetAllDoctors(DoctorSortFilterOptions options)
    {
        var doctors = _unitOfWork.DoctorRead.GetAll().AsNoTracking();
        var doctorDtos = doctors.Select(d => _mapper.Map<DoctorDto>(d)).AsPagedResult(options);
        return doctorDtos;
    }

    public PagedResult<DoctorDto> GetAllDoctorsWithRelations(DoctorSortFilterOptions options)
    {
        var doctors = _unitOfWork.DoctorRead.GetAllWithRelatedEntities();
        var doctorDtos = doctors.Select(d => _mapper.Map<DoctorDto>(d)).AsPagedResult(options);
        return doctorDtos;
    }

    /// <summary>
    /// Updates an existing doctor.
    /// </summary>
    /// <param name="doctor">The doctor DTO.</param>
    public async Task UpdateDoctor(UpdateDoctorDto doctor)
    {
        try
        {
            var validationResult = _updateDoctorValidator.Validate(doctor);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Start a transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var existingDoctor = await _unitOfWork.DoctorRead.GetByIdAsync(doctor.Id);
                if (existingDoctor == null)
                {
                    _logger.LogWarning("Doctor with ID {DoctorId} not found.", doctor.Id);
                    throw new KeyNotFoundException($"Doctor with ID {doctor.Id} not found.");
                }

                // Update related user information if necessary
                var userDto = new UpdateUserDto
                {
                    Id = existingDoctor.UserId,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    PhoneNumber = doctor.PhoneNumber,
                    BirthDate = doctor.BirthDate
                };

                await _userService.UpdateUser(userDto);

                // Map updated properties from DTO to entity
                _mapper.Map(doctor, existingDoctor);
                existingDoctor.UpdatedAt = DateTime.UtcNow;

                var isUpdated = _unitOfWork.DoctorWrite.Update(existingDoctor);

                if (!isUpdated)
                {
                    _logger.LogError("Doctor update failed for {DoctorId}.", doctor.Id);
                    await _unitOfWork.RollbackTransactionAsync();
                    throw new Exception($"Doctor update failed for {doctor.Id}");
                }

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();

                _logger.LogInformation("Doctor {DoctorId} updated successfully.", doctor.Id);
            }
            catch (Exception ex)
            {
                // Roll back the transaction on any error
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "Transaction failed while updating doctor.");
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating doctor {DoctorId}.", doctor.Id);
            throw;
        }
    }
}
