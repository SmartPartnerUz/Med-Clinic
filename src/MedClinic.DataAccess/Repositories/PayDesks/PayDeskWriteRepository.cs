using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.PayDesks;

namespace MedClinic.DataAccess.Repositories;

public class PayDeskWriteRepository : WriteRepository<PayDesk>, IPayDeskWriteRepository
{
    public PayDeskWriteRepository(AppDbContext context) : base(context)
    {
    }
}