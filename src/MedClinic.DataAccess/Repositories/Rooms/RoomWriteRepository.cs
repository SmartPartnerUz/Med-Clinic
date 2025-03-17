using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Rooms;

namespace MedClinic.DataAccess.Repositories;

public class RoomWriteRepository : WriteRepository<Room>, IRoomWriteRepository
{
    public RoomWriteRepository(AppDbContext context) : base(context)
    {
    }
}