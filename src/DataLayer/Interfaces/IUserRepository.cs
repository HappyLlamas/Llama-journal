using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User?> GetById(string userId);
    Task<User?> FindByEmail(string email);
    Task Update(User user);
    Task CreateUser(string email, string fullname, RoleEnum role);
    Task DeleteUser(User user);
}
}
