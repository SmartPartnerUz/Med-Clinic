using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing orders.
/// </summary>
public interface IOrderService
{
    Task<(bool, Guid)> CreateOrder(AddOrderDto order);
    Task UpdateOrder(UpdateOrderDto order);
    Task DeleteOrder(Guid id);
    PagedResult<OrderDto> GetAllOrders(OrderSortFilterOptions options);
    Task<OrderDto> GetOrderById(Guid id);
}