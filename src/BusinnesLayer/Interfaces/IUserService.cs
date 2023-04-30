using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
    Task SetUserGroup(string userId, int groupId);
    Task SetUserRole(string userId, RoleEnum role);
    Task CreateUser(string email, string password, RoleEnum role);
}
