namespace MedClinic.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IBedReadRepository BedRead { get; set; }
    IBedWriteRepository BedWrite { get; set; }

    IDoctorProfitReadRepository DoctorProfitRead { get; set; }
    IDoctorProfitWriteRepository DoctorProfitWrite { get; set; }

    IDoctorRoomReadRepository DoctorRoomRead { get; set; }
    IDoctorRoomWriteRepository DoctorRoomWrite { get; set; }

    IDoctorReadRepository DoctorRead { get; set; }
    IDoctorWriteRepository DoctorWrite { get; set; }

    IFirstViewOrderReadRepository FirstViewOrderRead { get; set; }
    IFirstViewOrderWriteRepository FirstViewOrderWrite { get; set; }

    IHospitalServiceReadRepository HospitalServiceRead { get; set; }
    IHospitalServiceWriteRepository HospitalServiceWrite { get; set; }

    ILaboratoryServiceReadRepository LaboratoryServiceRead { get; set; }
    ILaboratoryServiceWriteRepository LaboratoryServiceWrite { get; set; }

    IOrderReadRepository OrderRead { get; set; }
    IOrderWriteRepository OrderWrite { get; set; }

    IPatientReadRepository PatientRead { get; set; }
    IPatientWriteRepository PatientWrite { get; set; }

    IPayDeskReadRepository PayDeskRead { get; set; }
    IPayDeskWriteRepository PayDeskWrite { get; set; }

    IPositionReadRepository PositionRead { get; set; }
    IPositionWriteRepository PositionWrite { get; set; }

    IRoleReadRepository RoleRead { get; set; }
    IRoleWriteRepository RoleWrite { get; set; }

    IRoomReadRepository RoomRead { get; set; }
    IRoomWriteRepository RoomWrite { get; set; }

    IStatusReadRepository StatusRead { get; set; }
    IStatusWriteRepository StatusWrite { get; set; }

    IUserReadRepository UserRead { get; set; }
    IUserWriteRepository UserWrite { get; set; }

    Task<int> SaveChangesAsync();
}
