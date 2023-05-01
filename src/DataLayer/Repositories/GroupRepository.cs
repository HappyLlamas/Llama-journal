using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
public class GroupRepository : IGroupRepository
{
    private readonly ModelsContext _context;

    public GroupRepository(ModelsContext context)
    {
        _context = context;
    }

    public async Task<List<Group>> GetGroups()
    {
        return await _context.Groups.ToListAsync();
    }

    public async Task<Group?> GetById(long id)
    {
		return await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
    }
	public async Task<List<User>> GetUsersInGroup(Group group)
	{
		return await _context.Users.Include(u => u.Group).Where(u => u.Group.Id == group.Id).ToListAsync();
	}

    public async Task Add(Group group)
    {
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Group group)
    {
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Group group)
    {
        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();
    }
}
}
