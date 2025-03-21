using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing patients.
/// </summary>
public interface IPatientService
{
    Task<(bool, Guid)> CreatePatient(AddPatientDto patient);
    Task UpdatePatient(UpdatePatientDto patient);
    Task DeletePatient(Guid id);
    PagedResult<PatientDto> GetAllPatients(PatientSortFilterOptions options);
    Task<PatientDto> GetPatientById(Guid id);
}