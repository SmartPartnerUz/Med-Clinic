using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.HospitalServices;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for HospitalService.
/// </summary>
public class HospitalServiceProfile : Profile
{
    public HospitalServiceProfile()
    {
        // Mapping from AddHospitalServiceDto to HospitalService (for inserting into DB)
        CreateMap<AddHospitalServiceDto, HospitalService>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateHospitalServiceDto to HospitalService (for updating in DB)
        CreateMap<UpdateHospitalServiceDto, HospitalService>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from HospitalService to HospitalServiceDto (for returning data)
        CreateMap<HospitalService, HospitalServiceDto>();
    }
}