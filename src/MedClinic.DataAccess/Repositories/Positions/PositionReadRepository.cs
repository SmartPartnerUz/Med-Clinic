using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Positions;

namespace MedClinic.DataAccess.Repositories;

public class PositionReadRepository : ReadRepository<Position>, IPositionReadRepository
{
    public PositionReadRepository(AppDbContext context) : base(context)
    {
    }
}