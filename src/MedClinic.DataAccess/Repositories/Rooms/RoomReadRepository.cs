using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Rooms;

namespace MedClinic.DataAccess.Repositories;

public class RoomReadRepository : ReadRepository<Room>, IRoomReadRepository
{
    public RoomReadRepository(AppDbContext context) : base(context)
    {
    }
}