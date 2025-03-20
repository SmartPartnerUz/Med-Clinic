using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Users;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing users.
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddUserDto> _addUserValidator;
    private readonly IValidator<UpdateUserDto> _updateUserValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addUserValidator">The validator for adding a user.</param>
    /// <param name="updateUserValidator">The validator for updating a user.</param>
    public UserService(
        IUnitOfWork unitOfWork,
        ILogger<UserService> logger,
        IMapper mapper,
        IValidator<AddUserDto> addUserValidator,
        IValidator<UpdateUserDto> updateUserValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addUserValidator = addUserValidator;
        _updateUserValidator = updateUserValidator;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created user.</returns>
    public async Task<(bool, Guid)> CreateUser(AddUserDto user)
    {
        try
        {
            // Validate the user DTO
            var validationResult = _addUserValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to User entity
            var newUser = _mapper.Map<User>(user);
            newUser.Id = Guid.NewGuid();
            newUser.CreatedAt = DateTime.UtcNow;
            newUser.UpdatedAt = DateTime.UtcNow;

            // Add user to the repository
            var isCreated = await _unitOfWork.UserWrite.AddAsync(newUser);

            if (!isCreated)
            {
                _logger.LogError("User creation failed for {Id}.", newUser.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("User {UserId} created successfully.", newUser.Id);
            return (true, newUser.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("User creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the user.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    public async void DeleteUser(Guid id)
    {
        try
        {
            var user = await _unitOfWork.UserRead.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            _unitOfWork.UserWrite.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("User {UserId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting user with ID {UserId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all users with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of user DTOs.</returns>
    public PagedResult<UserDto> GetAllUsers(UserSortFilterOptions options)
    {
        var users = _unitOfWork.UserRead.GetAll().AsNoTracking();
        var userDtos = users.Select(u => _mapper.Map<UserDto>(u)).AsPagedResult(options);
        return userDtos;
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="user">The user DTO.</param>
    public async Task UpdateUser(UpdateUserDto user)
    {
        try
        {
            var validationResult = _updateUserValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _unitOfWork.UserRead.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", user.Id);
                throw new KeyNotFoundException($"User with ID {user.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(user, existingUser);
            existingUser.UpdatedAt = DateTime.UtcNow;

            var isUpdated = _unitOfWork.UserWrite.Update(existingUser);

            if (!isUpdated)
            {
                _logger.LogError("User update failed for {UserId}.", user.Id);
                throw new Exception($"User update failed for {user.Id}");
            }

            _logger.LogInformation("User {UserId} updated successfully.", user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating user {UserId}.", user.Id);
            throw;
        }
    }
}
