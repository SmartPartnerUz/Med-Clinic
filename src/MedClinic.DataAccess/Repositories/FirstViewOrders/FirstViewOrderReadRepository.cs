using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.FirstViewOrders;

namespace MedClinic.DataAccess.Repositories;

public class FirstViewOrderReadRepository : ReadRepository<FirstViewOrder>, IFirstViewOrderReadRepository
{
    public FirstViewOrderReadRepository(AppDbContext context) : base(context)
    {
    }
}