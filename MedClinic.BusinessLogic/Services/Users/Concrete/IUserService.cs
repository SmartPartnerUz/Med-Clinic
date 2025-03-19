using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

public interface IUserService
{
    Task<(bool, Guid)> CreateUser(AddUserDto user);
    PagedResult<UserDto> GetAllUsers(UserSortFilterOptions opions);
    Task UpdateUser(UpdateUserDto user);
    void DeleteUser(Guid id);
}
