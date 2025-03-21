using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing roles.
/// </summary>
public interface IRoleService
{
    Task<(bool, Guid)> CreateRole(AddRoleDto role);
    Task UpdateRole(UpdateRoleDto role);
    Task DeleteRole(Guid id);
    PagedResult<RoleDto> GetAllRoles(RoleSortFilterOptions options);
    Task<RoleDto> GetRoleById(Guid id);
}