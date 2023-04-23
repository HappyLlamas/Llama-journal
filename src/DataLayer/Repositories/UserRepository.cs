using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ModelsContext _context;

    public UserRepository(ModelsContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email)!;
    }

    public async Task Update(User user)
    {
        await _context.SaveChangesAsync();
    }

    public async Task CreateUser(string email, string fullname)
    {
        var user = new User { Email = email, FullName = fullname };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
