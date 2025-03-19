namespace MedClinic.DataAccess.Interfaces;

public interface IWriteRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    bool Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}