using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Doctors;

namespace MedClinic.BusinessLogic.Configurations;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        // Map AddDoctorDto to Doctor
        CreateMap<AddDoctorDto, Doctor>()
            .ForMember(dest => dest.User, opt => opt.Ignore()) // User will be set manually
            .ForMember(dest => dest.Position, opt => opt.Ignore()) // Position will be set manually
            .ForMember(dest => dest.DoctorRoom, opt => opt.Ignore()) // DoctorRoom will be set manually
            .ForMember(dest => dest.Role, opt => opt.Ignore()) // Role will be set manually
            .ForMember(dest => dest.HospitalService, opt => opt.Ignore()) // HospitalService will be set manually
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Password should be handled securely
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()); // Password should be handled securely

        // Map UpdateDoctorDto to Doctor
        CreateMap<UpdateDoctorDto, Doctor>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Position, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorRoom, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.HospitalService, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

        // Map Doctor to a DTO (if needed)
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.DoctorRoom, opt => opt.MapFrom(src => src.DoctorRoom))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.HospitalService, opt => opt.MapFrom(src => src.HospitalService))
            .ForMember(dest => dest.BedPercentage, opt => opt.MapFrom(src => src.BedPercentage)) // Explicitly mapped
            .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary)) // Explicitly mapped
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
    }
}