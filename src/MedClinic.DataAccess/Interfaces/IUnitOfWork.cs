namespace MedClinic.DataAccess.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBedReadRepository BedRead { get; }
    IBedWriteRepository BedWrite { get; }
    IDoctorProfitReadRepository DoctorProfitRead { get; }
    IDoctorProfitWriteRepository DoctorProfitWrite { get; }
    IDoctorRoomReadRepository DoctorRoomRead { get; }
    IDoctorRoomWriteRepository DoctorRoomWrite { get; }
    IDoctorReadRepository DoctorRead { get; }
    IDoctorWriteRepository DoctorWrite { get; }
    IFirstViewOrderReadRepository FirstViewOrderRead { get; }
    IFirstViewOrderWriteRepository FirstViewOrderWrite { get; }
    IHospitalServiceReadRepository HospitalServiceRead { get; }
    IHospitalServiceWriteRepository HospitalServiceWrite { get; }
    ILaboratoryServiceReadRepository LaboratoryServiceRead { get; }
    ILaboratoryServiceWriteRepository LaboratoryServiceWrite { get; }
    IOrderReadRepository OrderRead { get; }
    IOrderWriteRepository OrderWrite { get; }
    IPatientReadRepository PatientRead { get; }
    IPatientWriteRepository PatientWrite { get; }
    IPayDeskReadRepository PayDeskRead { get; }
    IPayDeskWriteRepository PayDeskWrite { get; }
    IPositionReadRepository PositionRead { get; }
    IPositionWriteRepository PositionWrite { get; }
    IRoleReadRepository RoleRead { get; }
    IRoleWriteRepository RoleWrite { get; }
    IRoomReadRepository RoomRead { get; }
    IRoomWriteRepository RoomWrite { get; }
    IStatusReadRepository StatusRead { get; }
    IStatusWriteRepository StatusWrite { get; }
    IUserReadRepository UserRead { get; }
    IUserWriteRepository UserWrite { get; }


    // Transaction methods (already present)
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<int> SaveChangesAsync();
}
