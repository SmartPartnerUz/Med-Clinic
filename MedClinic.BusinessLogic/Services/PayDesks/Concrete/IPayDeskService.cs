using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing pay desks.
/// </summary>
public interface IPayDeskService
{
    Task<(bool, Guid)> CreatePayDesk(AddPayDeskDto payDesk);
    Task UpdatePayDesk(UpdatePayDeskDto payDesk);
    Task DeletePayDesk(Guid id);
    PagedResult<PayDeskDto> GetAllPayDesks(PayDeskSortFilterOptions options);
    Task<PayDeskDto> GetPayDeskById(Guid id);
}