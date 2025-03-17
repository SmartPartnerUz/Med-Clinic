using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.PayDesks;

namespace MedClinic.DataAccess.Repositories;

public class PayDeskReadRepository : ReadRepository<PayDesk>, IPayDeskReadRepository
{
    public PayDeskReadRepository(AppDbContext context) : base(context)
    {
    }
}