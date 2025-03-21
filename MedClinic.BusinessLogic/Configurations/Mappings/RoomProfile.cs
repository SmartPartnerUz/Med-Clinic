using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Rooms;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for Room.
/// </summary>
public class RoomProfile : Profile
{
    public RoomProfile()
    {
        // Mapping from AddRoomDto to Room (for inserting into DB)
        CreateMap<AddRoomDto, Room>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateRoomDto to Room (for updating in DB)
        CreateMap<UpdateRoomDto, Room>();

        // Mapping from Room to RoomDto (for returning data)
        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.IsBusy, opt => opt.MapFrom(src => src.Beds.All(b => b.IsBusy)));
    }
}