using Business.Dtos;

namespace Business.Interfaces;

public interface IUserService
{
    Task<bool> CreateUserAsync(UserRegistrationForm form);
    Task<IEnumerable<User>> GetUsersAsync();
  //  Task<bool> DeleteUserByEmailAsync(string email);
    Task<bool> DeleteUserByIdAsync(int id);
    Task<User?> UpdateUserAsync(UserUpdateForm form, int id);
    Task<IEnumerable<User>> GetUsersWithManagerRoleAsync();

}