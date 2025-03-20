using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing laboratory services.
/// </summary>
public interface ILaboratoryServiceService
{
    Task<(bool, Guid)> CreateLaboratoryService(AddLaboratoryServiceDto laboratoryService);
    Task UpdateLaboratoryService(UpdateLaboratoryServiceDto laboratoryService);
    Task DeleteLaboratoryService(Guid id);
    PagedResult<LaboratoryServiceDto> GetAllLaboratoryServices(LaboratoryServiceSortFilterOptions options);
    Task<LaboratoryServiceDto> GetLaboratoryServiceById(Guid id);
}