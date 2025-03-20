using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing beds.
/// </summary>
public interface IBedService
{
    Task<(bool, Guid)> CreateBed(AddBedDto bed);
    Task DeleteBed(Guid id);
    PagedResult<BedDto> GetAllBeds(BedSortFilterOptions options);
    Task<BedDto> GetBedById(Guid id);
}
