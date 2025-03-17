using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Beds;

namespace MedClinic.DataAccess.Repositories;

public class BedReadRepository : ReadRepository<Bed>, IBedReadRepository
{
    public BedReadRepository(AppDbContext context) : base(context)
    {
        
    }
}
