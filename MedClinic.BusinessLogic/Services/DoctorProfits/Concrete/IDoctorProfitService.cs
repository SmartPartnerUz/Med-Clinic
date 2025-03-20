using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing doctor profits.
/// </summary>
public interface IDoctorProfitService
{
    Task<(bool, Guid)> CreateDoctorProfit(AddDoctorProfitDto doctorProfit);
    Task DeleteDoctorProfit(Guid id);
    PagedResult<DoctorProfitDto> GetAllDoctorProfits(DoctorProfitSortFilterOptions options);
    Task<DoctorProfitDto> GetDoctorProfitById(Guid id);
    Task UpdateDoctorProfit(UpdateDoctorProfitDto doctorProfit);
}