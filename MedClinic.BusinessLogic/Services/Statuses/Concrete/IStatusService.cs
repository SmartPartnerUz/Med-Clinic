using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing statuses.
/// </summary>
public interface IStatusService
{
    Task<(bool, Guid)> CreateStatus(AddStatusDto status);
    Task UpdateStatus(UpdateStatusDto status);
    Task DeleteStatus(Guid id);
    Task<StatusDto> GetStatusById(Guid id);
    PagedResult<StatusDto> GetAllStatuses(StatusSortFilterOptions options);
}
