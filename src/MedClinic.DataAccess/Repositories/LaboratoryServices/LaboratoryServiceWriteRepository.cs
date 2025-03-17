using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.LaboratoryServices;

namespace MedClinic.DataAccess.Repositories;

internal class LaboratoryServiceWriteRepository : WriteRepository<LaboratoryService>, ILaboratoryServiceWriteRepository
{
    public LaboratoryServiceWriteRepository(AppDbContext context) : base(context)
    {
    }
}