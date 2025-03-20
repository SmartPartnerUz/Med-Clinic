using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.LaboratoryServices;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing laboratory services.
/// </summary>
public class LaboratoryServiceService : ILaboratoryServiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LaboratoryServiceService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddLaboratoryServiceDto> _addLaboratoryServiceValidator;
    private readonly IValidator<UpdateLaboratoryServiceDto> _updateLaboratoryServiceValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="LaboratoryServiceService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addLaboratoryServiceValidator">The validator for adding a laboratory service.</param>
    /// <param name="updateLaboratoryServiceValidator">The validator for updating a laboratory service.</param>
    public LaboratoryServiceService(
        IUnitOfWork unitOfWork,
        ILogger<LaboratoryServiceService> logger,
        IMapper mapper,
        IValidator<AddLaboratoryServiceDto> addLaboratoryServiceValidator,
        IValidator<UpdateLaboratoryServiceDto> updateLaboratoryServiceValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addLaboratoryServiceValidator = addLaboratoryServiceValidator;
        _updateLaboratoryServiceValidator = updateLaboratoryServiceValidator;
    }

    /// <summary>
    /// Creates a new laboratory service.
    /// </summary>
    /// <param name="laboratoryService">The laboratory service DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created laboratory service.</returns>
    public async Task<(bool, Guid)> CreateLaboratoryService(AddLaboratoryServiceDto laboratoryService)
    {
        try
        {
            // Validate the laboratory service DTO
            var validationResult = _addLaboratoryServiceValidator.Validate(laboratoryService);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to LaboratoryService entity
            var newLaboratoryService = _mapper.Map<LaboratoryService>(laboratoryService);
            newLaboratoryService.Id = Guid.NewGuid();
            newLaboratoryService.CreatedAt = DateTime.UtcNow;
            newLaboratoryService.UpdatedAt = DateTime.UtcNow;

            // Add laboratory service to the repository
            var isCreated = await _unitOfWork.LaboratoryServiceWrite.AddAsync(newLaboratoryService);

            if (!isCreated)
            {
                _logger.LogError("Laboratory service creation failed for {Id}.", newLaboratoryService.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Laboratory service {LaboratoryServiceId} created successfully.", newLaboratoryService.Id);
            return (true, newLaboratoryService.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Laboratory service creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the laboratory service.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing laboratory service.
    /// </summary>
    /// <param name="laboratoryService">The laboratory service DTO.</param>
    public async Task UpdateLaboratoryService(UpdateLaboratoryServiceDto laboratoryService)
    {
        try
        {
            var validationResult = _updateLaboratoryServiceValidator.Validate(laboratoryService);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingLaboratoryService = await _unitOfWork.LaboratoryServiceRead.GetByIdAsync(laboratoryService.Id);
            if (existingLaboratoryService == null)
            {
                _logger.LogWarning("Laboratory service with ID {LaboratoryServiceId} not found.", laboratoryService.Id);
                throw new KeyNotFoundException($"Laboratory service with ID {laboratoryService.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(laboratoryService, existingLaboratoryService);
            existingLaboratoryService.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.LaboratoryServiceWrite.Update(existingLaboratoryService);

            if (!isUpdated)
            {
                _logger.LogError("Laboratory service update failed for {LaboratoryServiceId}.", laboratoryService.Id);
                throw new Exception($"Laboratory service update failed for {laboratoryService.Id}");
            }

            _logger.LogInformation("Laboratory service {LaboratoryServiceId} updated successfully.", laboratoryService.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating laboratory service {LaboratoryServiceId}.", laboratoryService.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a laboratory service by ID.
    /// </summary>
    /// <param name="id">The laboratory service ID.</param>
    public async Task DeleteLaboratoryService(Guid id)
    {
        try
        {
            var laboratoryService = await _unitOfWork.LaboratoryServiceRead.GetByIdAsync(id);
            if (laboratoryService == null)
            {
                _logger.LogWarning("Laboratory service with ID {LaboratoryServiceId} not found.", id);
                throw new KeyNotFoundException($"Laboratory service with ID {id} not found.");
            }

            _unitOfWork.LaboratoryServiceWrite.Delete(laboratoryService);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Laboratory service {LaboratoryServiceId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting laboratory service with ID {LaboratoryServiceId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all laboratory services with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of laboratory service DTOs.</returns>
    public PagedResult<LaboratoryServiceDto> GetAllLaboratoryServices(LaboratoryServiceSortFilterOptions options)
    {
        var laboratoryServices = _unitOfWork.LaboratoryServiceRead.GetAll().AsNoTracking();
        var laboratoryServiceDtos = laboratoryServices.Select(ls => _mapper.Map<LaboratoryServiceDto>(ls)).AsPagedResult(options);
        return laboratoryServiceDtos;
    }

    /// <summary>
    /// Gets a laboratory service by ID.
    /// </summary>
    /// <param name="id">The laboratory service ID.</param>
    /// <returns>The laboratory service DTO.</returns>
    public async Task<LaboratoryServiceDto> GetLaboratoryServiceById(Guid id)
    {
        var laboratoryService = await _unitOfWork.LaboratoryServiceRead.GetByIdAsync(id);
        if (laboratoryService == null)
        {
            _logger.LogWarning("Laboratory service with ID {LaboratoryServiceId} not found.", id);
            throw new KeyNotFoundException($"Laboratory service with ID {id} not found.");
        }

        return _mapper.Map<LaboratoryServiceDto>(laboratoryService);
    }
}