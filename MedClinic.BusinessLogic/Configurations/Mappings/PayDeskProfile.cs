using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.PayDesks;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for PayDesk.
/// </summary>
public class PayDeskProfile : Profile
{
    public PayDeskProfile()
    {
        // Mapping from AddPayDeskDto to PayDesk (for inserting into DB)
        CreateMap<AddPayDeskDto, PayDesk>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdatePayDeskDto to PayDesk (for updating in DB)
        CreateMap<UpdatePayDeskDto, PayDesk>();

        // Mapping from PayDesk to PayDeskDto (for returning data)
        CreateMap<PayDesk, PayDeskDto>();
    }
}