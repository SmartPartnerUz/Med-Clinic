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

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
}
