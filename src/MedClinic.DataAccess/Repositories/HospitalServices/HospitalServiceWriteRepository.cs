using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.HospitalServices;

namespace MedClinic.DataAccess.Repositories;

public class HospitalServiceWriteRepository : WriteRepository<HospitalService>, IHospitalServiceWriteRepository
{
    public HospitalServiceWriteRepository(AppDbContext context) : base(context)
    {
    }
}