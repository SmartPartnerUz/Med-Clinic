using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

public interface IUserService
{
    Task CreateUser(AddUserDto user);
    PagedResult<UserDto> GetAllUsers(UserSortFilterOptions opions);
    Task UpdateUser(UpdateDto user);
    void DeleteUser(Guid id);
}
