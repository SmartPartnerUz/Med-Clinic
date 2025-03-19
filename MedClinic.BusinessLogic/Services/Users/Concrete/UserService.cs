using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Users;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

public class UserService(IUnitOfWork unitOfWork,
                         ILogger<UserService> logger,
                         IMapper mapper,
                         IValidator<AddUserDto> addUserValidator,
                         IValidator<UpdateDto> updateUserValidator) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<UserService> _logger = logger;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<AddUserDto> _addUserValidator = addUserValidator;
    private readonly IValidator<UpdateDto> _updateUserValidator = updateUserValidator;

    public async Task CreateUser(AddUserDto user)
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
            newUser.CreatedAt = DateTime.UtcNow;
            newUser.UpdatedAt = DateTime.UtcNow;

            // Add user to the repository
            var isCreated = await _unitOfWork.UserWrite.AddAsync(newUser);

            if (!isCreated)
            {
                _logger.LogError("User creation failed for {Id}.", newUser.Id);
                throw new Exception($"User creation failed! {newUser.Id}");
            }

            _logger.LogInformation("User {UserId} created successfully.", newUser.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("User creation failed due to validation errors: {Errors}", ex.Errors);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the user.");
            throw;
        }
    }

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

    public PagedResult<UserDto> GetAllUsers(UserSortFilterOptions opions)
    {
        var users = _unitOfWork.UserRead.GetAll().AsNoTracking();
        var userDtos = users.Select(u => _mapper.Map<UserDto>(u)).AsPagedResult(opions);
        return userDtos;
    }

    public async Task UpdateUser(UpdateDto user)
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
