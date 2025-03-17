using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.PayDesks;

namespace MedClinic.DataAccess.Repositories;

public class PayDeskWrietRepository : WriteRepository<PayDesk>, IPayDeskWrietRepository
{
    public PayDeskWrietRepository(AppDbContext context) : base(context)
    {
    }
}