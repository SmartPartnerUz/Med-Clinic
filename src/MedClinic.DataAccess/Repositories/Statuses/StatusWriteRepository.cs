using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Statuses;

namespace MedClinic.DataAccess.Repositories;

public class StatusWriteRepository : WriteRepository<Status>, IStatusWriteRepository
{
    public StatusWriteRepository(AppDbContext context) : base(context)
    {
    }
}