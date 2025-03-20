using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing first view orders.
/// </summary>
public interface IFirstViewOrderService
{
    Task<(bool, Guid)> CreateFirstViewOrder(AddFirstViewOrderDto firstViewOrder);
    Task UpdateFirstViewOrder(UpdateFirstViewOrderDto firstViewOrder);
    Task DeleteFirstViewOrder(Guid id);
    PagedResult<FirstViewOrderDto> GetAllFirstViewOrders(FirstViewOrderSortFilterOptions options);
    Task<FirstViewOrderDto> GetFirstViewOrderById(Guid id);
}