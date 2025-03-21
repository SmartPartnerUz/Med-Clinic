using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Positions;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing positions.
/// </summary>
public class PositionService : IPositionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PositionService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPositionDto> _addPositionValidator;
    private readonly IValidator<UpdatePositionDto> _updatePositionValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="PositionService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addPositionValidator">The validator for adding a position.</param>
    /// <param name="updatePositionValidator">The validator for updating a position.</param>
    public PositionService(
        IUnitOfWork unitOfWork,
        ILogger<PositionService> logger,
        IMapper mapper,
        IValidator<AddPositionDto> addPositionValidator,
        IValidator<UpdatePositionDto> updatePositionValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addPositionValidator = addPositionValidator;
        _updatePositionValidator = updatePositionValidator;
    }

    /// <summary>
    /// Creates a new position.
    /// </summary>
    /// <param name="position">The position DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created position.</returns>
    public async Task<(bool, Guid)> CreatePosition(AddPositionDto position)
    {
        try
        {
            // Validate the position DTO
            var validationResult = _addPositionValidator.Validate(position);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Position entity
            var newPosition = _mapper.Map<Position>(position);
            newPosition.Id = Guid.NewGuid();
            newPosition.CreatedAt = DateTime.UtcNow;

            // Add position to the repository
            var isCreated = await _unitOfWork.PositionWrite.AddAsync(newPosition);

            if (!isCreated)
            {
                _logger.LogError("Position creation failed for {Id}.", newPosition.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Position {PositionId} created successfully.", newPosition.Id);
            return (true, newPosition.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Position creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the position.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing position.
    /// </summary>
    /// <param name="position">The position DTO.</param>
    public async Task UpdatePosition(UpdatePositionDto position)
    {
        try
        {
            var validationResult = _updatePositionValidator.Validate(position);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingPosition = await _unitOfWork.PositionRead.GetByIdAsync(position.Id);
            if (existingPosition == null)
            {
                _logger.LogWarning("Position with ID {PositionId} not found.", position.Id);
                throw new KeyNotFoundException($"Position with ID {position.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(position, existingPosition);

            var isUpdated = _unitOfWork.PositionWrite.Update(existingPosition);

            if (!isUpdated)
            {
                _logger.LogError("Position update failed for {PositionId}.", position.Id);
                throw new Exception($"Position update failed for {position.Id}");
            }

            _logger.LogInformation("Position {PositionId} updated successfully.", position.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating position {PositionId}.", position.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a position by ID.
    /// </summary>
    /// <param name="id">The position ID.</param>
    public async Task DeletePosition(Guid id)
    {
        try
        {
            var position = await _unitOfWork.PositionRead.GetByIdAsync(id);
            if (position == null)
            {
                _logger.LogWarning("Position with ID {PositionId} not found.", id);
                throw new KeyNotFoundException($"Position with ID {id} not found.");
            }

            _unitOfWork.PositionWrite.Delete(position);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Position {PositionId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting position with ID {PositionId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all positions with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of position DTOs.</returns>
    public PagedResult<PositionDto> GetAllPositions(PositionSortFilterOptions options)
    {
        var positions = _unitOfWork.PositionRead.GetAll().AsNoTracking();
        var positionDtos = positions.Select(p => _mapper.Map<PositionDto>(p)).AsPagedResult(options);
        return positionDtos;
    }

    /// <summary>
    /// Gets a position by ID.
    /// </summary>
    /// <param name="id">The position ID.</param>
    /// <returns>The position DTO.</returns>
    public async Task<PositionDto> GetPositionById(Guid id)
    {
        var position = await _unitOfWork.PositionRead.GetByIdAsync(id);
        if (position == null)
        {
            _logger.LogWarning("Position with ID {PositionId} not found.", id);
            throw new KeyNotFoundException($"Position with ID {id} not found.");
        }

        return _mapper.Map<PositionDto>(position);
    }
}