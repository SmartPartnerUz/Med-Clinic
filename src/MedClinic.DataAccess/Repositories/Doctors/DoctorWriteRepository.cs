using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Doctors;

namespace MedClinic.DataAccess.Repositories;

public class DoctorWriteRepository : WriteRepository<Doctor>, IDoctorWriteRepository
{
    public DoctorWriteRepository(AppDbContext context) : base(context)
    {
    }
}