using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace MedClinic.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        // Initialize repositories
        BedRead = new BedReadRepository(_dbContext);
        BedWrite = new BedWriteRepository(_dbContext);
        DoctorRead = new DoctorReadRepository(_dbContext);
        DoctorWrite = new DoctorWriteRepository(_dbContext);
        DoctorRoomRead = new DoctorRoomReadRepository(_dbContext);
        DoctorRoomWrite = new DoctorRoomWriteRepository(_dbContext);
        DoctorProfitRead = new DoctorProfitReadRepository(_dbContext);
        DoctorProfitWrite = new DoctorProfitWriteRepository(_dbContext);
        FirstViewOrderRead = new FirstViewOrderReadRepository(_dbContext);
        FirstViewOrderWrite = new FirstViewOrderWriteRepository(_dbContext);
        HospitalServiceRead = new HospitalServiceReadRepository(_dbContext);
        HospitalServiceWrite = new HospitalServiceWriteRepository(_dbContext);
        LaboratoryServiceRead = new LaboratoryServiceReadRepository(_dbContext);
        LaboratoryServiceWrite = new LaboratoryServiceWriteRepository(_dbContext);
        OrderRead = new OrderReadRepository(_dbContext);
        OrderWrite = new OrderWriteRepository(_dbContext);
        PatientRead = new PatientReadRepository(_dbContext);
        PatientWrite = new PatientWriteRepository(_dbContext);
        PayDeskRead = new PayDeskReadRepository(_dbContext);
        PayDeskWrite = new PayDeskWriteRepository(_dbContext);
        PositionRead = new PositionReadRepository(_dbContext);
        PositionWrite = new PositionWriteRepository(_dbContext);
        RoleRead = new RoleReadRepository(_dbContext);
        RoleWrite = new RoleWriteRepository(_dbContext);
        RoomRead = new RoomReadRepository(_dbContext);
        RoomWrite = new RoomWriteRepository(_dbContext);
        StatusRead = new StatusReadRepository(_dbContext);
        StatusWrite = new StatusWriteRepository(_dbContext);
        UserRead = new UserReadRepository(_dbContext);
        UserWrite = new UserWriteRepository(_dbContext);
    }

    public IBedReadRepository BedRead { get; }
    public IBedWriteRepository BedWrite { get; }
    public IDoctorReadRepository DoctorRead { get; }
    public IDoctorWriteRepository DoctorWrite { get; }
    public IDoctorRoomReadRepository DoctorRoomRead { get; }
    public IDoctorRoomWriteRepository DoctorRoomWrite { get; }
    public IDoctorProfitReadRepository DoctorProfitRead { get; }
    public IDoctorProfitWriteRepository DoctorProfitWrite { get; }
    public IFirstViewOrderReadRepository FirstViewOrderRead { get; }
    public IFirstViewOrderWriteRepository FirstViewOrderWrite { get; }
    public IHospitalServiceReadRepository HospitalServiceRead { get; }
    public IHospitalServiceWriteRepository HospitalServiceWrite { get; }
    public ILaboratoryServiceReadRepository LaboratoryServiceRead { get; }
    public ILaboratoryServiceWriteRepository LaboratoryServiceWrite { get; }
    public IOrderReadRepository OrderRead { get; }
    public IOrderWriteRepository OrderWrite { get; }
    public IPatientReadRepository PatientRead { get; }
    public IPatientWriteRepository PatientWrite { get; }
    public IPayDeskReadRepository PayDeskRead { get; }
    public IPayDeskWriteRepository PayDeskWrite { get; }
    public IPositionReadRepository PositionRead { get; }
    public IPositionWriteRepository PositionWrite { get; }
    public IRoleReadRepository RoleRead { get; }
    public IRoleWriteRepository RoleWrite { get; }
    public IRoomReadRepository RoomRead { get; }
    public IRoomWriteRepository RoomWrite { get; }
    public IStatusReadRepository StatusRead { get; }
    public IStatusWriteRepository StatusWrite { get; }
    public IUserReadRepository UserRead { get; }
    public IUserWriteRepository UserWrite { get; }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}