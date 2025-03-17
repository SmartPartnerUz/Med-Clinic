using System.Linq.Expressions;

namespace MedClinic.DataAccess.Interfaces;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
}