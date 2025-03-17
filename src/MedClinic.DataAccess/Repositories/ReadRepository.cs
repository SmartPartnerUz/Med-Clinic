using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedClinic.DataAccess.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public IQueryable<T> GetAll() => _dbSet;
    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        => _dbSet.Where(predicate);
}