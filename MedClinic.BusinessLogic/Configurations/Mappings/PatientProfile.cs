using AutoMapper;
using MedClinic.BusinessLogic.Services;
using MedClinic.Domain.Entities.Patients;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Mapping profile for Patient.
/// </summary>
public class PatientProfile : Profile
{
    public PatientProfile()
    {
        // Mapping from AddPatientDto to Patient (for inserting into DB)
        CreateMap<AddPatientDto, Patient>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // Mapping from UpdatePatientDto to Patient (for updating in DB)
        CreateMap<UpdatePatientDto, Patient>();

        // Mapping from Patient to PatientDto (for returning data)
        CreateMap<Patient, PatientDto>();
    }
}