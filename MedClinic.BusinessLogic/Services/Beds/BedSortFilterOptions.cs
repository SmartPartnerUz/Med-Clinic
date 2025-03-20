using MedClinic.Domain.Models.SortFilter;

namespace MedClinic.BusinessLogic.Services;

public class BedSortFilterOptions : SortFilterPageOptions
{
    public Guid? RoomId { get; set; }
}
