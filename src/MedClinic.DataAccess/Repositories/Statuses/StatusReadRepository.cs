using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Statuses;

namespace MedClinic.DataAccess.Repositories;

public class StatusReadRepository : ReadRepository<Status>, IStatusReadRepository
{
    public StatusReadRepository(AppDbContext context) : base(context)
    {
    }
}