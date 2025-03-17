using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorProfits;

namespace MedClinic.DataAccess.Repositories;

public class DoctorProfitReadRepository : ReadRepository<DoctorProfit>, IDoctorProfitReadRepository
{
    public DoctorProfitReadRepository(AppDbContext context) : base(context)
    {
    }
}
