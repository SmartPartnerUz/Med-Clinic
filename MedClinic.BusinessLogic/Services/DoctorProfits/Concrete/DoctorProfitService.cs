using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorProfits;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing doctor profits.
/// </summary>
public class DoctorProfitService : IDoctorProfitService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DoctorProfitService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddDoctorProfitDto> _addDoctorProfitValidator;
    private readonly IValidator<UpdateDoctorProfitDto> _updateDoctorProfitValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoctorProfitService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addDoctorProfitValidator">The validator for adding a doctor profit.</param>
    /// <param name="updateDoctorProfitValidator">The validator for updating a doctor profit.</param>
    public DoctorProfitService(
        IUnitOfWork unitOfWork,
        ILogger<DoctorProfitService> logger,
        IMapper mapper,
        IValidator<AddDoctorProfitDto> addDoctorProfitValidator,
        IValidator<UpdateDoctorProfitDto> updateDoctorProfitValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addDoctorProfitValidator = addDoctorProfitValidator;
        _updateDoctorProfitValidator = updateDoctorProfitValidator;
    }

    /// <summary>
    /// Creates a new doctor profit.
    /// </summary>
    /// <param name="doctorProfit">The doctor profit DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created doctor profit.</returns>
    public async Task<(bool, Guid)> CreateDoctorProfit(AddDoctorProfitDto doctorProfit)
    {
        try
        {
            // Validate the doctor profit DTO
            var validationResult = _addDoctorProfitValidator.Validate(doctorProfit);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to DoctorProfit entity
            var newDoctorProfit = _mapper.Map<DoctorProfit>(doctorProfit);
            newDoctorProfit.Id = Guid.NewGuid();
            newDoctorProfit.CreatedAt = DateTime.UtcNow;
            newDoctorProfit.UpdatedAt = DateTime.UtcNow;

            // Add doctor profit to the repository
            var isCreated = await _unitOfWork.DoctorProfitWrite.AddAsync(newDoctorProfit);

            if (!isCreated)
            {
                _logger.LogError("Doctor profit creation failed for {Id}.", newDoctorProfit.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Doctor profit {DoctorProfitId} created successfully.", newDoctorProfit.Id);
            return (true, newDoctorProfit.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Doctor profit creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the doctor profit.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing doctor profit.
    /// </summary>
    /// <param name="doctorProfit">The doctor profit DTO.</param>
    public async Task UpdateDoctorProfit(UpdateDoctorProfitDto doctorProfit)
    {
        try
        {
            var validationResult = _updateDoctorProfitValidator.Validate(doctorProfit);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingDoctorProfit = await _unitOfWork.DoctorProfitRead.GetByIdAsync(doctorProfit.Id);
            if (existingDoctorProfit == null)
            {
                _logger.LogWarning("Doctor profit with ID {DoctorProfitId} not found.", doctorProfit.Id);
                throw new KeyNotFoundException($"Doctor profit with ID {doctorProfit.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(doctorProfit, existingDoctorProfit);
            existingDoctorProfit.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.DoctorProfitWrite.Update(existingDoctorProfit);

            if (!isUpdated)
            {
                _logger.LogError("Doctor profit update failed for {DoctorProfitId}.", doctorProfit.Id);
                throw new Exception($"Doctor profit update failed for {doctorProfit.Id}");
            }

            _logger.LogInformation("Doctor profit {DoctorProfitId} updated successfully.", doctorProfit.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating doctor profit {DoctorProfitId}.", doctorProfit.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a doctor profit by ID.
    /// </summary>
    /// <param name="id">The doctor profit ID.</param>
    public async Task DeleteDoctorProfit(Guid id)
    {
        try
        {
            var doctorProfit = await _unitOfWork.DoctorProfitRead.GetByIdAsync(id);
            if (doctorProfit == null)
            {
                _logger.LogWarning("Doctor profit with ID {DoctorProfitId} not found.", id);
                throw new KeyNotFoundException($"Doctor profit with ID {id} not found.");
            }

            _unitOfWork.DoctorProfitWrite.Delete(doctorProfit);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Doctor profit {DoctorProfitId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting doctor profit with ID {DoctorProfitId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all doctor profits with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of doctor profit DTOs.</returns>
    public PagedResult<DoctorProfitDto> GetAllDoctorProfits(DoctorProfitSortFilterOptions options)
    {
        var doctorProfits = _unitOfWork.DoctorProfitRead.GetAll().AsNoTracking();
        var doctorProfitDtos = doctorProfits.Select(dp => _mapper.Map<DoctorProfitDto>(dp)).AsPagedResult(options);
        return doctorProfitDtos;
    }

    /// <summary>
    /// Gets a doctor profit by ID.
    /// </summary>
    /// <param name="id">The doctor profit ID.</param>
    /// <returns>The doctor profit DTO.</returns>
    public async Task<DoctorProfitDto> GetDoctorProfitById(Guid id)
    {
        var doctorProfit = await _unitOfWork.DoctorProfitRead.GetByIdAsync(id);
        if (doctorProfit == null)
        {
            _logger.LogWarning("Doctor profit with ID {DoctorProfitId} not found.", id);
            throw new KeyNotFoundException($"Doctor profit with ID {id} not found.");
        }

        return _mapper.Map<DoctorProfitDto>(doctorProfit);
    }
}

