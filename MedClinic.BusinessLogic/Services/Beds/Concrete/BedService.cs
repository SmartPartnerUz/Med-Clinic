using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Beds;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing beds.
/// </summary>
public class BedService : IBedService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BedService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddBedDto> _addBedValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BedService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addBedValidator">The validator for adding a bed.</param>
    /// <param name="updateBedValidator">The validator for updating a bed.</param>
    public BedService(
        IUnitOfWork unitOfWork,
        ILogger<BedService> logger,
        IMapper mapper,
        IValidator<AddBedDto> addBedValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addBedValidator = addBedValidator;
    }

    /// <summary>
    /// Creates a new bed.
    /// </summary>
    /// <param name="bed">The bed DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created bed.</returns>
    public async Task<(bool, Guid)> CreateBed(AddBedDto bed)
    {
        try
        {
            // Validate the bed DTO
            var validationResult = _addBedValidator.Validate(bed);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Bed entity
            var newBed = _mapper.Map<Bed>(bed);
            newBed.Id = Guid.NewGuid();
            newBed.CreatedAt = DateTime.UtcNow;

            // Add bed to the repository
            var isCreated = await _unitOfWork.BedWrite.AddAsync(newBed);

            if (!isCreated)
            {
                _logger.LogError("Bed creation failed for {Id}.", newBed.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Bed {BedId} created successfully.", newBed.Id);
            return (true, newBed.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Bed creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the bed.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Deletes a bed by ID.
    /// </summary>
    /// <param name="id">The bed ID.</param>
    public async Task DeleteBed(Guid id)
    {
        try
        {
            var bed = await _unitOfWork.BedRead.GetByIdAsync(id);
            if (bed == null)
            {
                _logger.LogWarning("Bed with ID {BedId} not found.", id);
                throw new KeyNotFoundException($"Bed with ID {id} not found.");
            }

            _unitOfWork.BedWrite.Delete(bed);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Bed {BedId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting bed with ID {BedId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all beds with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of bed DTOs.</returns>
    public PagedResult<BedDto> GetAllBeds(BedSortFilterOptions options)
    {
        var beds = _unitOfWork.BedRead.GetAll().AsNoTracking().SortFilter(options);
        var bedDtos = beds.Select(b => _mapper.Map<BedDto>(b)).AsPagedResult(options);
        return bedDtos;
    }

    /// <summary>
    /// Gets a bed by ID.
    /// </summary>
    /// <param name="id">The bed ID.</param>
    /// <returns>The bed DTO.</returns>
    public async Task<BedDto> GetBedById(Guid id)
    {
        var bed = await _unitOfWork.BedRead.GetByIdAsync(id);
        if (bed == null)
        {
            _logger.LogWarning("Bed with ID {BedId} not found.", id);
            throw new KeyNotFoundException($"Bed with ID {id} not found.");
        }

        return _mapper.Map<BedDto>(bed);
    }
}
