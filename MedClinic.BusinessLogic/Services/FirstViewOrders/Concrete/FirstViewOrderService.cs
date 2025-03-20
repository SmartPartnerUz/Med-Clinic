using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.FirstViewOrders;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing first view orders.
/// </summary>
public class FirstViewOrderService : IFirstViewOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FirstViewOrderService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddFirstViewOrderDto> _addFirstViewOrderValidator;
    private readonly IValidator<UpdateFirstViewOrderDto> _updateFirstViewOrderValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FirstViewOrderService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addFirstViewOrderValidator">The validator for adding a first view order.</param>
    /// <param name="updateFirstViewOrderValidator">The validator for updating a first view order.</param>
    public FirstViewOrderService(
        IUnitOfWork unitOfWork,
        ILogger<FirstViewOrderService> logger,
        IMapper mapper,
        IValidator<AddFirstViewOrderDto> addFirstViewOrderValidator,
        IValidator<UpdateFirstViewOrderDto> updateFirstViewOrderValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addFirstViewOrderValidator = addFirstViewOrderValidator;
        _updateFirstViewOrderValidator = updateFirstViewOrderValidator;
    }

    /// <summary>
    /// Creates a new first view order.
    /// </summary>
    /// <param name="firstViewOrder">The first view order DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created first view order.</returns>
    public async Task<(bool, Guid)> CreateFirstViewOrder(AddFirstViewOrderDto firstViewOrder)
    {
        try
        {
            // Validate the first view order DTO
            var validationResult = _addFirstViewOrderValidator.Validate(firstViewOrder);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to FirstViewOrder entity
            var newFirstViewOrder = _mapper.Map<FirstViewOrder>(firstViewOrder);
            newFirstViewOrder.Id = Guid.NewGuid();
            newFirstViewOrder.CreatedAt = DateTime.UtcNow;
            newFirstViewOrder.UpdatedAt = DateTime.UtcNow;

            // Add first view order to the repository
            var isCreated = await _unitOfWork.FirstViewOrderWrite.AddAsync(newFirstViewOrder);

            if (!isCreated)
            {
                _logger.LogError("First view order creation failed for {Id}.", newFirstViewOrder.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("First view order {FirstViewOrderId} created successfully.", newFirstViewOrder.Id);
            return (true, newFirstViewOrder.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("First view order creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the first view order.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing first view order.
    /// </summary>
    /// <param name="firstViewOrder">The first view order DTO.</param>
    public async Task UpdateFirstViewOrder(UpdateFirstViewOrderDto firstViewOrder)
    {
        try
        {
            var validationResult = _updateFirstViewOrderValidator.Validate(firstViewOrder);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingFirstViewOrder = await _unitOfWork.FirstViewOrderRead.GetByIdAsync(firstViewOrder.Id);
            if (existingFirstViewOrder == null)
            {
                _logger.LogWarning("First view order with ID {FirstViewOrderId} not found.", firstViewOrder.Id);
                throw new KeyNotFoundException($"First view order with ID {firstViewOrder.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(firstViewOrder, existingFirstViewOrder);
            existingFirstViewOrder.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.FirstViewOrderWrite.Update(existingFirstViewOrder);

            if (!isUpdated)
            {
                _logger.LogError("First view order update failed for {FirstViewOrderId}.", firstViewOrder.Id);
                throw new Exception($"First view order update failed for {firstViewOrder.Id}");
            }

            _logger.LogInformation("First view order {FirstViewOrderId} updated successfully.", firstViewOrder.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating first view order {FirstViewOrderId}.", firstViewOrder.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a first view order by ID.
    /// </summary>
    /// <param name="id">The first view order ID.</param>
    public async Task DeleteFirstViewOrder(Guid id)
    {
        try
        {
            var firstViewOrder = await _unitOfWork.FirstViewOrderRead.GetByIdAsync(id);
            if (firstViewOrder == null)
            {
                _logger.LogWarning("First view order with ID {FirstViewOrderId} not found.", id);
                throw new KeyNotFoundException($"First view order with ID {id} not found.");
            }

            _unitOfWork.FirstViewOrderWrite.Delete(firstViewOrder);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("First view order {FirstViewOrderId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting first view order with ID {FirstViewOrderId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all first view orders with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of first view order DTOs.</returns>
    public PagedResult<FirstViewOrderDto> GetAllFirstViewOrders(FirstViewOrderSortFilterOptions options)
    {
        var firstViewOrders = _unitOfWork.FirstViewOrderRead.GetAll().AsNoTracking();
        var firstViewOrderDtos = firstViewOrders.Select(fvo => _mapper.Map<FirstViewOrderDto>(fvo)).AsPagedResult(options);
        return firstViewOrderDtos;
    }

    /// <summary>
    /// Gets a first view order by ID.
    /// </summary>
    /// <param name="id">The first view order ID.</param>
    /// <returns>The first view order DTO.</returns>
    public async Task<FirstViewOrderDto> GetFirstViewOrderById(Guid id)
    {
        var firstViewOrder = await _unitOfWork.FirstViewOrderRead.GetByIdAsync(id);
        if (firstViewOrder == null)
        {
            _logger.LogWarning("First view order with ID {FirstViewOrderId} not found.", id);
            throw new KeyNotFoundException($"First view order with ID {id} not found.");
        }

        return _mapper.Map<FirstViewOrderDto>(firstViewOrder);
    }
}