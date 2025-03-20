using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Beds;

namespace MedClinic.BusinessLogic.Configurations;

public class BedProfile : Profile
{
    public BedProfile()
    {
        // Mapping from AddBedDto to Bed (for inserting into DB)
        CreateMap<AddBedDto, Bed>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from Bed to BedDto (for returning data)
        CreateMap<Bed, BedDto>();
    }
}
