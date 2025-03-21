using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Positions;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for Position.
/// </summary>
public class PositionProfile : Profile
{
    public PositionProfile()
    {
        // Mapping from AddPositionDto to Position (for inserting into DB)
        CreateMap<AddPositionDto, Position>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdatePositionDto to Position (for updating in DB)
        CreateMap<UpdatePositionDto, Position>();

        // Mapping from Position to PositionDto (for returning data)
        CreateMap<Position, PositionDto>();
    }
}