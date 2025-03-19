using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;

namespace MedClinic.DataAccess.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork, IDisposable
{
    public IBedReadRepository BedRead { get; set; } = new BedReadRepository(dbContext);
    public IBedWriteRepository BedWrite { get; set; } = new BedWriteRepository(dbContext);

    public IDoctorReadRepository DoctorRead { get; set; } = new DoctorReadRepository(dbContext);
    public IDoctorWriteRepository DoctorWrite { get; set; } = new DoctorWriteRepository(dbContext);

    public IDoctorRoomReadRepository DoctorRoomRead { get; set; } = new DoctorRoomReadRepository(dbContext);
    public IDoctorRoomWriteRepository DoctorRoomWrite { get; set; } = new DoctorRoomWriteRepository(dbContext);

    public IDoctorProfitReadRepository DoctorProfitRead { get; set; } = new DoctorProfitReadRepository(dbContext);
    public IDoctorProfitWriteRepository DoctorProfitWrite { get; set; } = new DoctorProfitWriteRepository(dbContext);

    public IFirstViewOrderReadRepository FirstViewOrderRead { get; set; } = new FirstViewOrderReadRepository(dbContext);
    public IFirstViewOrderWriteRepository FirstViewOrderWrite { get; set; } = new FirstViewOrderWriteRepository(dbContext);

    public IHospitalServiceReadRepository HospitalServiceRead { get; set; } = new HospitalServiceReadRepository(dbContext);
    public IHospitalServiceWriteRepository HospitalServiceWrite { get; set; } = new HospitalServiceWriteRepository(dbContext);

    public ILaboratoryServiceReadRepository LaboratoryServiceRead { get; set; } = new LaboratoryServiceReadRepository(dbContext);
    public ILaboratoryServiceWriteRepository LaboratoryServiceWrite { get; set; } = new LaboratoryServiceWriteRepository(dbContext);

    public IOrderReadRepository OrderRead { get; set; } = new OrderReadRepository(dbContext);
    public IOrderWriteRepository OrderWrite { get; set; } = new OrderWriteRepository(dbContext);

    public IPatientReadRepository PatientRead { get; set; } = new PatientReadRepository(dbContext);
    public IPatientWriteRepository PatientWrite { get; set; } = new PatientWriteRepository(dbContext);

    public IPayDeskReadRepository PayDeskRead { get; set; } = new PayDeskReadRepository(dbContext);
    public IPayDeskWriteRepository PayDeskWrite { get; set; } = new PayDeskWriteRepository(dbContext);

    public IPositionReadRepository PositionRead { get; set; } = new PositionReadRepository(dbContext);
    public IPositionWriteRepository PositionWrite { get; set; } = new PositionWriteRepository(dbContext);

    public IRoleReadRepository RoleRead { get; set; } = new RoleReadRepository(dbContext);
    public IRoleWriteRepository RoleWrite { get; set; } = new RoleWriteRepository(dbContext);

    public IRoomReadRepository RoomRead { get; set; } = new RoomReadRepository(dbContext);
    public IRoomWriteRepository RoomWrite { get; set; } = new RoomWriteRepository(dbContext);

    public IStatusReadRepository StatusRead { get; set; } = new StatusReadRepository(dbContext);
    public IStatusWriteRepository StatusWrite { get; set; } = new StatusWriteRepository(dbContext);

    public IUserReadRepository UserRead { get; set; } = new UserReadRepository(dbContext);
    public IUserWriteRepository UserWrite { get; set; } = new UserWriteRepository(dbContext);

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

