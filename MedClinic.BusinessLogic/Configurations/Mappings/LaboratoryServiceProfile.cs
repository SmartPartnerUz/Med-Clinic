using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.LaboratoryServices;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for LaboratoryService.
/// </summary>
public class LaboratoryServiceProfile : Profile
{
    public LaboratoryServiceProfile()
    {
        // Mapping from AddLaboratoryServiceDto to LaboratoryService (for inserting into DB)
        CreateMap<AddLaboratoryServiceDto, LaboratoryService>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateLaboratoryServiceDto to LaboratoryService (for updating in DB)
        CreateMap<UpdateLaboratoryServiceDto, LaboratoryService>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from LaboratoryService to LaboratoryServiceDto (for returning data)
        CreateMap<LaboratoryService, LaboratoryServiceDto>();
    }
}