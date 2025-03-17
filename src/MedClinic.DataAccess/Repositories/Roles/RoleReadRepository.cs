using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Roles;

namespace MedClinic.DataAccess.Repositories;

public class RoleReadRepository : ReadRepository<Role>, IRoleReadRepository
{
    public RoleReadRepository(AppDbContext context) : base(context)
    {
    }
}