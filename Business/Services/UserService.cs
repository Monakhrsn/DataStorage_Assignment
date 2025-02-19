using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    
    //CREATE
    public async Task<bool> CreateUserAsync(UserRegistrationForm form)
    {
        var user = await GetUserEntityAsync(u => u.UserEmail == form.Email);
        if (user != null)
            return false;

        user = new UserEntity()
        {
            UserFirstName = form.FirstName,
            UserLastName = form.LastName,
            UserEmail = form.Email,
        };
        
        var result = await _userRepository.CreateAsync(user);
        return result != null;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new User(
            u.Id,
            u.UserFirstName,
            u.UserLastName,
            u.UserEmail,
            u.Role.RoleName,
            u.RoleId));
    }
    
    
    // GET USERS WITH MANAGER ROLE
    public async Task<IEnumerable<User>> GetUsersWithManagerRoleAsync()
    {
        var users = await _userRepository.GetUsersByRoleAsync(1);
        return users.Select(u => new User(
            u.Id,
            u.UserFirstName,
            u.UserLastName,
            u.UserEmail,
            u.Role.RoleName,
            u.RoleId
        ));
    }
    
    public Task<bool> DeleteUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> UpdateUserAsync(UserUpdateForm form, int id)
    {
        var user = await GetUserEntityAsync(x => x.Id == id);
        if (user == null)
            return null;       
        
        user.UserFirstName= form.FirstName;
        user.UserLastName = form.LastName;
        user.UserEmail = form.Email;

        user = await _userRepository.UpdateOneAsync(u => u.Id == id, user);
        return new User(
        user.Id,
        user.UserFirstName,
        user.UserLastName,
        user.UserEmail,
        user.Role.RoleName,
        user.RoleId
        );
    }
    
    private async Task<UserEntity?> GetUserEntityAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var user = await _userRepository.GetOneAsync(predicate);
        return user;
    }
}
