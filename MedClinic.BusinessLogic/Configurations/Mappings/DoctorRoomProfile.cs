using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.DoctorRooms;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for DoctorRoom.
/// </summary>
public class DoctorRoomProfile : Profile
{
    public DoctorRoomProfile()
    {
        // Mapping from AddDoctorRoomDto to DoctorRoom (for inserting into DB)
        CreateMap<AddDoctorRoomDto, DoctorRoom>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateDoctorRoomDto to DoctorRoom (for updating in DB)
        CreateMap<UpdateDoctorRoomDto, DoctorRoom>();

        // Mapping from DoctorRoom to DoctorRoomDto (for returning data)
        CreateMap<DoctorRoom, DoctorRoomDto>();
    }
}