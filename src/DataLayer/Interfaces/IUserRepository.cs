using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User?> GetById(string userId);
    Task<User?> FindByEmail(string email);
    Task Update(User user);
    Task CreateUser(User user);
    Task DeleteUser(User user);
}
}
