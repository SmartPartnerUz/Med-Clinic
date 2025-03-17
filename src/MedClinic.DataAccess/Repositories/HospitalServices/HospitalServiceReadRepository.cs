using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.HospitalServices;

namespace MedClinic.DataAccess.Repositories;

public class HospitalServiceReadRepository : ReadRepository<HospitalService>, IHospitalServiceReadRepository
{
    public HospitalServiceReadRepository(AppDbContext context) : base(context)
    {
    }
}