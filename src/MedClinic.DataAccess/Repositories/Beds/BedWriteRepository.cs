using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Beds;

namespace MedClinic.DataAccess.Repositories;

public class BedWriteRepository : WriteRepository<Bed>, IBedWriteRepository
{
    public BedWriteRepository(AppDbContext context) : base(context)
    {
    }
}
