using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Statuses;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

public class StatusService : IStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StatusService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddStatusDto> _addStatusValidator;
    private readonly IValidator<UpdateStatusDto> _updateStatusValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addStatusValidator">The validator for adding a status.</param>
    /// <param name="updateStatusValidator">The validator for updating a status.</param>
    public StatusService(
        IUnitOfWork unitOfWork,
        ILogger<StatusService> logger,
        IMapper mapper,
        IValidator<AddStatusDto> addStatusValidator,
        IValidator<UpdateStatusDto> updateStatusValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addStatusValidator = addStatusValidator;
        _updateStatusValidator = updateStatusValidator;
    }

    /// <summary>
    /// Creates a new status.
    /// </summary>
    /// <param name="status">The status DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created status.</returns>
    public async Task<(bool, Guid)> CreateStatus(AddStatusDto status)
    {
        try
        {
            // Validate the status DTO
            var validationResult = _addStatusValidator.Validate(status);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Status entity
            var newStatus = _mapper.Map<Status>(status);
            newStatus.Id = Guid.NewGuid();
            newStatus.CreatedAt = DateTime.UtcNow;

            // Add status to the repository
            var isCreated = await _unitOfWork.StatusWrite.AddAsync(newStatus);

            if (!isCreated)
            {
                _logger.LogError("Status creation failed for {Id}.", newStatus.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Status {StatusId} created successfully.", newStatus.Id);
            return (true, newStatus.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Status creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the status.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing status.
    /// </summary>
    /// <param name="status">The status DTO.</param>
    public async Task UpdateStatus(UpdateStatusDto status)
    {
        try
        {
            // Validate the status DTO
            var validationResult = _updateStatusValidator.Validate(status);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Retrieve the existing status from the repository
            var existingStatus = await _unitOfWork.StatusRead.GetByIdAsync(status.Id);
            if (existingStatus == null)
            {
                _logger.LogWarning("Status with ID {StatusId} not found.", status.Id);
                throw new KeyNotFoundException($"Status with ID {status.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(status, existingStatus);

            // Update the status in the repository
            var isUpdated = _unitOfWork.StatusWrite.Update(existingStatus);

            if (!isUpdated)
            {
                _logger.LogError("Status update failed for {StatusId}.", status.Id);
                throw new Exception($"Status update failed for {status.Id}");
            }

            _logger.LogInformation("Status {StatusId} updated successfully.", status.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating status {StatusId}.", status.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a status by ID.
    /// </summary>
    /// <param name="id">The status ID.</param>
    public async Task DeleteStatus(Guid id)
    {
        try
        {
            // Retrieve the status from the repository
            var status = await _unitOfWork.StatusRead.GetByIdAsync(id);
            if (status == null)
            {
                _logger.LogWarning("Status with ID {StatusId} not found.", id);
                throw new KeyNotFoundException($"Status with ID {id} not found.");
            }

            // Delete the status from the repository
            _unitOfWork.StatusWrite.Delete(status);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Status {StatusId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting status with ID {StatusId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets a status by ID.
    /// </summary>
    /// <param name="id">The status ID.</param>
    /// <returns>The status DTO.</returns>
    public async Task<StatusDto> GetStatusById(Guid id)
    {
        // Retrieve the status from the repository
        var status = await _unitOfWork.StatusRead.GetByIdAsync(id);
        if (status == null)
        {
            _logger.LogWarning("Status with ID {StatusId} not found.", id);
            throw new KeyNotFoundException($"Status with ID {id} not found.");
        }

        // Map the status entity to a DTO
        return _mapper.Map<StatusDto>(status);
    }

    /// <summary>
    /// Gets all statuses.
    /// </summary>
    /// <returns>A collection of status DTOs.</returns>
    public PagedResult<StatusDto> GetAllStatuses(StatusSortFilterOptions options)
    {
        // Retrieve all statuses from the repository
        var statuses = _unitOfWork.StatusRead.GetAll().AsNoTracking();
        var statusDtos = statuses.Select(s => _mapper.Map<StatusDto>(s)).AsPagedResult(options);
        return statusDtos;
    }
}
