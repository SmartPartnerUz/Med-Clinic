using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.HospitalServices;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing hospital services.
/// </summary>
public class HospitalServiceService : IHospitalServiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HospitalServiceService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddHospitalServiceDto> _addHospitalServiceValidator;
    private readonly IValidator<UpdateHospitalServiceDto> _updateHospitalServiceValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="HospitalServiceService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addHospitalServiceValidator">The validator for adding a hospital service.</param>
    /// <param name="updateHospitalServiceValidator">The validator for updating a hospital service.</param>
    public HospitalServiceService(
        IUnitOfWork unitOfWork,
        ILogger<HospitalServiceService> logger,
        IMapper mapper,
        IValidator<AddHospitalServiceDto> addHospitalServiceValidator,
        IValidator<UpdateHospitalServiceDto> updateHospitalServiceValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addHospitalServiceValidator = addHospitalServiceValidator;
        _updateHospitalServiceValidator = updateHospitalServiceValidator;
    }

    /// <summary>
    /// Creates a new hospital service.
    /// </summary>
    /// <param name="hospitalService">The hospital service DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created hospital service.</returns>
    public async Task<(bool, Guid)> CreateHospitalService(AddHospitalServiceDto hospitalService)
    {
        try
        {
            // Validate the hospital service DTO
            var validationResult = _addHospitalServiceValidator.Validate(hospitalService);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to HospitalService entity
            var newHospitalService = _mapper.Map<HospitalService>(hospitalService);
            newHospitalService.Id = Guid.NewGuid();
            newHospitalService.CreatedAt = DateTime.UtcNow;
            newHospitalService.UpdatedAt = DateTime.UtcNow;

            // Add hospital service to the repository
            var isCreated = await _unitOfWork.HospitalServiceWrite.AddAsync(newHospitalService);

            if (!isCreated)
            {
                _logger.LogError("Hospital service creation failed for {Id}.", newHospitalService.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Hospital service {HospitalServiceId} created successfully.", newHospitalService.Id);
            return (true, newHospitalService.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Hospital service creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the hospital service.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing hospital service.
    /// </summary>
    /// <param name="hospitalService">The hospital service DTO.</param>
    public async Task UpdateHospitalService(UpdateHospitalServiceDto hospitalService)
    {
        try
        {
            var validationResult = _updateHospitalServiceValidator.Validate(hospitalService);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingHospitalService = await _unitOfWork.HospitalServiceRead.GetByIdAsync(hospitalService.Id);
            if (existingHospitalService == null)
            {
                _logger.LogWarning("Hospital service with ID {HospitalServiceId} not found.", hospitalService.Id);
                throw new KeyNotFoundException($"Hospital service with ID {hospitalService.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(hospitalService, existingHospitalService);
            existingHospitalService.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.HospitalServiceWrite.Update(existingHospitalService);

            if (!isUpdated)
            {
                _logger.LogError("Hospital service update failed for {HospitalServiceId}.", hospitalService.Id);
                throw new Exception($"Hospital service update failed for {hospitalService.Id}");
            }

            _logger.LogInformation("Hospital service {HospitalServiceId} updated successfully.", hospitalService.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating hospital service {HospitalServiceId}.", hospitalService.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a hospital service by ID.
    /// </summary>
    /// <param name="id">The hospital service ID.</param>
    public async Task DeleteHospitalService(Guid id)
    {
        try
        {
            var hospitalService = await _unitOfWork.HospitalServiceRead.GetByIdAsync(id);
            if (hospitalService == null)
            {
                _logger.LogWarning("Hospital service with ID {HospitalServiceId} not found.", id);
                throw new KeyNotFoundException($"Hospital service with ID {id} not found.");
            }

            _unitOfWork.HospitalServiceWrite.Delete(hospitalService);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Hospital service {HospitalServiceId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting hospital service with ID {HospitalServiceId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all hospital services with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of hospital service DTOs.</returns>
    public PagedResult<HospitalServiceDto> GetAllHospitalServices(HospitalServiceSortFilterOptions options)
    {
        var hospitalServices = _unitOfWork.HospitalServiceRead.GetAll().AsNoTracking();
        var hospitalServiceDtos = hospitalServices.Select(hs => _mapper.Map<HospitalServiceDto>(hs)).AsPagedResult(options);
        return hospitalServiceDtos;
    }

    /// <summary>
    /// Gets a hospital service by ID.
    /// </summary>
    /// <param name="id">The hospital service ID.</param>
    /// <returns>The hospital service DTO.</returns>
    public async Task<HospitalServiceDto> GetHospitalServiceById(Guid id)
    {
        var hospitalService = await _unitOfWork.HospitalServiceRead.GetByIdAsync(id);
        if (hospitalService == null)
        {
            _logger.LogWarning("Hospital service with ID {HospitalServiceId} not found.", id);
            throw new KeyNotFoundException($"Hospital service with ID {id} not found.");
        }

        return _mapper.Map<HospitalServiceDto>(hospitalService);
    }
}