using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.LaboratoryServices;

namespace MedClinic.DataAccess.Repositories;

public class LaboratoryServiceReadRepository : ReadRepository<LaboratoryService>, ILaboratoryServiceReadRepository
{
    public LaboratoryServiceReadRepository(AppDbContext context) : base(context)
    {
    }
}