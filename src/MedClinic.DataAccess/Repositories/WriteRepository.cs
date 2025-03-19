using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedClinic.DataAccess.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }
    public bool Update(T entity)
    {
        _dbSet.Update(entity);
        return _context.SaveChanges() > 0;
    }
    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}
