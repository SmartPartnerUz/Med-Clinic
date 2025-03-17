using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorProfits;

namespace MedClinic.DataAccess.Repositories;

public class DoctorProfitWriteRepository : WriteRepository<DoctorProfit>, IDoctorProfitWriteRepository
{
    public DoctorProfitWriteRepository(AppDbContext context) : base(context)
    {
    }
}
