using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Roles;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for Role.
/// </summary>
public class RoleProfile : Profile
{
    public RoleProfile()
    {
        // Mapping from AddRoleDto to Role (for inserting into DB)
        CreateMap<AddRoleDto, Role>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdateRoleDto to Role (for updating in DB)
        CreateMap<UpdateRoleDto, Role>();

        // Mapping from Role to RoleDto (for returning data)
        CreateMap<Role, RoleDto>();
    }
}