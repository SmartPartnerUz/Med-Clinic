using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorRooms;

namespace MedClinic.DataAccess.Repositories;

public class DoctorRoomWriteRepository : WriteRepository<DoctorRoom>, IDoctorRoomWriteRepository
{
    public DoctorRoomWriteRepository(AppDbContext context) : base(context)
    {
    }
}