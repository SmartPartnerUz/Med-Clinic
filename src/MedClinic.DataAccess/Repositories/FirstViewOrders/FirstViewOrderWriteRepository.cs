using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.FirstViewOrders;

namespace MedClinic.DataAccess.Repositories;

public class FirstViewOrderWriteRepository : WriteRepository<FirstViewOrder>, IFirstViewOrderWriteRepository
{
    public FirstViewOrderWriteRepository(AppDbContext context) : base(context)
    {
    }
}