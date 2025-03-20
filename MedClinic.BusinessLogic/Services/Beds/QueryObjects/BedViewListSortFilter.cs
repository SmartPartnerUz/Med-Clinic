using MedClinic.BusinessLogic.Extentions;
using MedClinic.Domain.Entities.Beds;
using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

public static class BedViewListSortFilter
{
    public static IQueryable<Bed> SortFilter(this IQueryable<Bed> beds, BedSortFilterOptions filter)
    {
        var result = beds.Where(bd => bd.RoomId == filter.RoomId);
        return result;
    }
}
