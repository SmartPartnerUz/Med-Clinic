using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.DoctorProfits;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for DoctorProfit.
/// </summary>
public class DoctorProfitProfile : Profile
{
    public DoctorProfitProfile()
    {
        // Mapping from AddDoctorProfitDto to DoctorProfit (for inserting into DB)
        CreateMap<AddDoctorProfitDto, DoctorProfit>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from DoctorProfit to AddDoctorProfitDto (if needed)
        CreateMap<DoctorProfit, AddDoctorProfitDto>();
    }
}