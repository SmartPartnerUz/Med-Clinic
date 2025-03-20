using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing hospital services.
/// </summary>
public interface IHospitalServiceService
{
    Task<(bool, Guid)> CreateHospitalService(AddHospitalServiceDto hospitalService);
    Task UpdateHospitalService(UpdateHospitalServiceDto hospitalService);
    Task DeleteHospitalService(Guid id);
    PagedResult<HospitalServiceDto> GetAllHospitalServices(HospitalServiceSortFilterOptions options);
    Task<HospitalServiceDto> GetHospitalServiceById(Guid id);
}