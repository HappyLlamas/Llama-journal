using System.Security.Cryptography;
using DataLayer.Models;

namespace DataLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ModelsContext _context;

    public UserRepository(ModelsContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetById(string userId)
    {
        return _context.Users.Find(userId);
    }

    public User? FindByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email)!;
    }

    public void SetGroup(User user, string groupName)
    {
        user.Group = _context.Groups.FirstOrDefault(g => g.Name == groupName)!;
        _context.SaveChanges();
    }

    public void SetRole(User user, RoleEnum role)
    {
        user.Role = role;
        _context.SaveChanges();
    }

    public void CreateUser(string email, string fullname)
    {
        var user = new User { Email = email, FullName = fullname };
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void SetUserPassword(User user, string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(salt);
        }
        // TODO: add password hash
        // user.Password = hashedPassword;

        _context.SaveChanges();
    }

    public bool CheckPassword(User user, string password)
    {
        // byte[] salt = Convert.FromBase64String(user.PasswordSalt);
        // TODO: add password hash
        string hashedPassword = password;
        return user.Password == hashedPassword;
    }
    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
