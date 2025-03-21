using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing rooms.
/// </summary>
public interface IRoomService
{
    Task<(bool, Guid)> CreateRoom(AddRoomDto room);
    Task UpdateRoom(UpdateRoomDto room);
    Task DeleteRoom(Guid id);
    PagedResult<RoomDto> GetAllRooms(RoomSortFilterOptions options);
    Task<RoomDto> GetRoomById(Guid id);
}