using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Orders;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for Order.
/// </summary>
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Mapping from AddOrderDto to Order (for inserting into DB)
        CreateMap<AddOrderDto, Order>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateOrderDto to Order (for updating in DB)
        CreateMap<UpdateOrderDto, Order>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from Order to OrderDto (for returning data)
        CreateMap<Order, OrderDto>();
    }
}