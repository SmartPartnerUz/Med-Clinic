using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Orders;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddOrderDto> _addOrderValidator;
    private readonly IValidator<UpdateOrderDto> _updateOrderValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addOrderValidator">The validator for adding an order.</param>
    /// <param name="updateOrderValidator">The validator for updating an order.</param>
    public OrderService(
        IUnitOfWork unitOfWork,
        ILogger<OrderService> logger,
        IMapper mapper,
        IValidator<AddOrderDto> addOrderValidator,
        IValidator<UpdateOrderDto> updateOrderValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addOrderValidator = addOrderValidator;
        _updateOrderValidator = updateOrderValidator;
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The order DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created order.</returns>
    public async Task<(bool, Guid)> CreateOrder(AddOrderDto order)
    {
        try
        {
            // Validate the order DTO
            var validationResult = _addOrderValidator.Validate(order);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Order entity
            var newOrder = _mapper.Map<Order>(order);
            newOrder.Id = Guid.NewGuid();
            newOrder.CreatedAt = DateTime.UtcNow;
            newOrder.UpdatedAt = DateTime.UtcNow;

            // Add order to the repository
            var isCreated = await _unitOfWork.OrderWrite.AddAsync(newOrder);

            if (!isCreated)
            {
                _logger.LogError("Order creation failed for {Id}.", newOrder.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Order {OrderId} created successfully.", newOrder.Id);
            return (true, newOrder.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Order creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the order.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing order.
    /// </summary>
    /// <param name="order">The order DTO.</param>
    public async Task UpdateOrder(UpdateOrderDto order)
    {
        try
        {
            var validationResult = _updateOrderValidator.Validate(order);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingOrder = await _unitOfWork.OrderRead.GetByIdAsync(order.Id);
            if (existingOrder == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found.", order.Id);
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(order, existingOrder);
            existingOrder.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.OrderWrite.Update(existingOrder);

            if (!isUpdated)
            {
                _logger.LogError("Order update failed for {OrderId}.", order.Id);
                throw new Exception($"Order update failed for {order.Id}");
            }

            _logger.LogInformation("Order {OrderId} updated successfully.", order.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating order {OrderId}.", order.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes an order by ID.
    /// </summary>
    /// <param name="id">The order ID.</param>
    public async Task DeleteOrder(Guid id)
    {
        try
        {
            var order = await _unitOfWork.OrderRead.GetByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found.", id);
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            _unitOfWork.OrderWrite.Delete(order);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Order {OrderId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting order with ID {OrderId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all orders with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of order DTOs.</returns>
    public PagedResult<OrderDto> GetAllOrders(OrderSortFilterOptions options)
    {
        var orders = _unitOfWork.OrderRead.GetAll().AsNoTracking();
        var orderDtos = orders.Select(o => _mapper.Map<OrderDto>(o)).AsPagedResult(options);
        return orderDtos;
    }

    /// <summary>
    /// Gets an order by ID.
    /// </summary>
    /// <param name="id">The order ID.</param>
    /// <returns>The order DTO.</returns>
    public async Task<OrderDto> GetOrderById(Guid id)
    {
        var order = await _unitOfWork.OrderRead.GetByIdAsync(id);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found.", id);
            throw new KeyNotFoundException($"Order with ID {id} not found.");
        }

        return _mapper.Map<OrderDto>(order);
    }
}