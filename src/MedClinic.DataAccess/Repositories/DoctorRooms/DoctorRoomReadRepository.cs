using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorRooms;

namespace MedClinic.DataAccess.Repositories;

public class DoctorRoomReadRepository : ReadRepository<DoctorRoom>, IDoctorRoomReadRepository
{
    public DoctorRoomReadRepository(AppDbContext context) : base(context)
    {
    }
}