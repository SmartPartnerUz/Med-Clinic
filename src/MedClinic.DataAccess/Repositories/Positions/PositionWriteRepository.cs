using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Positions;

namespace MedClinic.DataAccess.Repositories;

public class PositionWriteRepository : WriteRepository<Position>, IPositionWriteRepository
{
    public PositionWriteRepository(AppDbContext context) : base(context)
    {
    }
}