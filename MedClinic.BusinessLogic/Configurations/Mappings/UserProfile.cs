using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Users;

namespace MedClinic.BusinessLogic.Configurations;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapping from AddUserDto to User (for inserting into DB)
        CreateMap<AddUserDto, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from User to AddUserDto (if needed)
        CreateMap<User, AddUserDto>();
    }
}
