using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Roles;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing roles.
/// </summary>
public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddRoleDto> _addRoleValidator;
    private readonly IValidator<UpdateRoleDto> _updateRoleValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoleService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addRoleValidator">The validator for adding a role.</param>
    /// <param name="updateRoleValidator">The validator for updating a role.</param>
    public RoleService(
        IUnitOfWork unitOfWork,
        ILogger<RoleService> logger,
        IMapper mapper,
        IValidator<AddRoleDto> addRoleValidator,
        IValidator<UpdateRoleDto> updateRoleValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addRoleValidator = addRoleValidator;
        _updateRoleValidator = updateRoleValidator;
    }

    /// <summary>
    /// Creates a new role.
    /// </summary>
    /// <param name="role">The role DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created role.</returns>
    public async Task<(bool, Guid)> CreateRole(AddRoleDto role)
    {
        try
        {
            // Validate the role DTO
            var validationResult = _addRoleValidator.Validate(role);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Role entity
            var newRole = _mapper.Map<Role>(role);
            newRole.Id = Guid.NewGuid();
            newRole.CreatedAt = DateTime.UtcNow;

            // Add role to the repository
            var isCreated = await _unitOfWork.RoleWrite.AddAsync(newRole);

            if (!isCreated)
            {
                _logger.LogError("Role creation failed for {Id}.", newRole.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Role {RoleId} created successfully.", newRole.Id);
            return (true, newRole.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Role creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the role.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing role.
    /// </summary>
    /// <param name="role">The role DTO.</param>
    public async Task UpdateRole(UpdateRoleDto role)
    {
        try
        {
            var validationResult = _updateRoleValidator.Validate(role);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingRole = await _unitOfWork.RoleRead.GetByIdAsync(role.Id);
            if (existingRole == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", role.Id);
                throw new KeyNotFoundException($"Role with ID {role.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(role, existingRole);

            var isUpdated = _unitOfWork.RoleWrite.Update(existingRole);

            if (!isUpdated)
            {
                _logger.LogError("Role update failed for {RoleId}.", role.Id);
                throw new Exception($"Role update failed for {role.Id}");
            }

            _logger.LogInformation("Role {RoleId} updated successfully.", role.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating role {RoleId}.", role.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a role by ID.
    /// </summary>
    /// <param name="id">The role ID.</param>
    public async Task DeleteRole(Guid id)
    {
        try
        {
            var role = await _unitOfWork.RoleRead.GetByIdAsync(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID {RoleId} not found.", id);
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }

            _unitOfWork.RoleWrite.Delete(role);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Role {RoleId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting role with ID {RoleId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all roles with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of role DTOs.</returns>
    public PagedResult<RoleDto> GetAllRoles(RoleSortFilterOptions options)
    {
        var roles = _unitOfWork.RoleRead.GetAll().AsNoTracking();
        var roleDtos = roles.Select(r => _mapper.Map<RoleDto>(r)).AsPagedResult(options);
        return roleDtos;
    }

    /// <summary>
    /// Gets a role by ID.
    /// </summary>
    /// <param name="id">The role ID.</param>
    /// <returns>The role DTO.</returns>
    public async Task<RoleDto> GetRoleById(Guid id)
    {
        var role = await _unitOfWork.RoleRead.GetByIdAsync(id);
        if (role == null)
        {
            _logger.LogWarning("Role with ID {RoleId} not found.", id);
            throw new KeyNotFoundException($"Role with ID {id} not found.");
        }

        return _mapper.Map<RoleDto>(role);
    }
}