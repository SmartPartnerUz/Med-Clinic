using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.FirstViewOrders;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for FirstViewOrder.
/// </summary>
public class FirstViewOrderProfile : Profile
{
    public FirstViewOrderProfile()
    {
        // Mapping from AddFirstViewOrderDto to FirstViewOrder (for inserting into DB)
        CreateMap<AddFirstViewOrderDto, FirstViewOrder>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateFirstViewOrderDto to FirstViewOrder (for updating in DB)
        CreateMap<UpdateFirstViewOrderDto, FirstViewOrder>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from FirstViewOrder to FirstViewOrderDto (for returning data)
        CreateMap<FirstViewOrder, FirstViewOrderDto>();
    }
}