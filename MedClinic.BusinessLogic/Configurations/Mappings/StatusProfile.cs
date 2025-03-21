using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Statuses;

namespace MedClinic.BusinessLogic.Configurations;

public class StatusProfile : Profile
{
    public StatusProfile()
    {
        CreateMap<AddStatusDto, Status>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<UpdateStatusDto, Status>();

        CreateMap<Status, StatusDto>();
    }
}
