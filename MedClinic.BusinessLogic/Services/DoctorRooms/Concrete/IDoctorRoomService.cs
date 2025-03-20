using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing doctor rooms.
/// </summary>
public interface IDoctorRoomService
{
    Task<(bool, Guid)> CreateDoctorRoom(AddDoctorRoomDto doctorRoom);
    Task UpdateDoctorRoom(UpdateDoctorRoomDto doctorRoom);
    Task DeleteDoctorRoom(Guid id);
    PagedResult<DoctorRoomDto> GetAllDoctorRooms(DoctorRoomSortFilterOptions options);
    Task<DoctorRoomDto> GetDoctorRoomById(Guid id);
}