using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Users;

namespace MedClinic.DataAccess.Repositories;

public class UserReadRepository : ReadRepository<User>, IUserReadRepository
{
    public UserReadRepository(AppDbContext context) : base(context)
    {
    }
}