using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

public interface IDoctorService
{
    Task<(bool, Guid)> CreateDoctor(AddDoctorDto doctor);
    PagedResult<DoctorDto> GetAllDoctors(DoctorSortFilterOptions options);
    Task UpdateDoctor(UpdateDoctorDto doctor);
    void DeleteDoctor(Guid id);
}
