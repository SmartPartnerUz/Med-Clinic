using FluentValidation;
using MedClinic.BusinessLogic.Configurations.Validators;
using MedClinic.BusinessLogic.Services;
using MedClinic.DataAccess.Data;
using MedClinic.DataAccess.Interfaces;
using MedClinic.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MedClinic.BusinessLogic.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddLoggingConfiguration();

        #region Validators
        services.AddScoped<IValidator<AddDoctorDto>, AddDoctorDtoValidator>();
        services.AddScoped<IValidator<UpdateDoctorDto>, UpdateDoctorDtoValidator>();

        services.AddScoped<IValidator<AddUserDto>, AddUserDtoValidator>();
        services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

        services.AddScoped<IValidator<AddRoleDto>, AddRoleValidator>();
        services.AddScoped<IValidator<UpdateRoleDto>, UpdateRoleValidator>();

        services.AddScoped<IValidator<AddDoctorRoomDto>, AddDoctorRoomDtoValidator>();
        services.AddScoped<IValidator<UpdateDoctorRoomDto>, UpdateDoctorRoomDtoValidator>();

        services.AddScoped<IValidator<AddBedDto>, AddBedValidator>();

        services.AddScoped<IValidator<AddDoctorProfitDto>, AddDoctorProfitValidator>();
        services.AddScoped<IValidator<UpdateDoctorProfitDto>, UpdateDoctorProfitValidator>();

        services.AddScoped<IValidator<AddFirstViewOrderDto>, AddFirstViewOrderValidator>();
        services.AddScoped<IValidator<UpdateFirstViewOrderDto>, UpdateFirstViewOrderValidator>();

        services.AddScoped<IValidator<AddHospitalServiceDto>, AddHospitalServiceValidator>();
        services.AddScoped<IValidator<UpdateHospitalServiceDto>, UpdateHospitalServiceValidator>();

        services.AddScoped<IValidator<AddLaboratoryServiceDto>, AddLaboratoryServiceValidator>();
        services.AddScoped<IValidator<UpdateLaboratoryServiceDto>, UpdateLaboratoryServiceValidator>();

        services.AddScoped<IValidator<AddOrderDto>, AddOrderValidator>();
        services.AddScoped<IValidator<UpdateOrderDto>, UpdateOrderValidator>();

        services.AddScoped<IValidator<AddPatientDto>, AddPatientValidator>();
        services.AddScoped<IValidator<UpdatePatientDto>, UpdatePatientValidator>();

        services.AddScoped<IValidator<AddPayDeskDto>, AddPayDeskValidator>();
        services.AddScoped<IValidator<UpdatePayDeskDto>, UpdatePayDeskValidator>();

        services.AddScoped<IValidator<AddPositionDto>, AddPositionValidator>();
        services.AddScoped<IValidator<UpdatePositionDto>, UpdatePositionValidator>();

        services.AddScoped<IValidator<AddRoomDto>, AddRoomValidator>();
        services.AddScoped<IValidator<UpdateRoomDto>, UpdateRoomValidator>();

        services.AddScoped<IValidator<AddStatusDto>, AddStatusValidator>();
        services.AddScoped<IValidator<UpdateStatusDto>, UpdateStatusValidator>();
        #endregion

        #region DB context configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        #endregion

        #region Dependency Injection
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IBedService, BedService>();
        services.AddScoped<IDoctorProfitService, DoctorProfitService>();
        services.AddScoped<IDoctorRoomService, DoctorRoomService>();
        services.AddScoped<IFirstViewOrderService, FirstViewOrderService>();
        services.AddScoped<IHospitalServiceService, HospitalServiceService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IPayDeskService, PayDeskService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IStatusService, StatusService>();
        #endregion

        return services;
    }
}
