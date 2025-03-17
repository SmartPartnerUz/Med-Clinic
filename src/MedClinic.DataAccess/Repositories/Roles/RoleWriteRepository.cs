using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Roles;

namespace MedClinic.DataAccess.Repositories;

public class RoleWriteRepository : WriteRepository<Role>, IRoleWriteRepository
{
    public RoleWriteRepository(AppDbContext context) : base(context)
    {
    }
}