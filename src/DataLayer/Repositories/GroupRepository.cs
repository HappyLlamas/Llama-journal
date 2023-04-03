using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
public class GroupRepository : IGroupRepository
{
	private readonly ModelsContext _context;

	public GroupRepository(ModelsContext context)
	{
		_context = context;
	}

	public List<Group> GetGroups()
	{
		return _context.Groups.ToList();
	}

	public Group GetById(long id)
	{
		return _context.Groups.FirstOrDefault(g => g.Id == id);
	}

	public void Add(Group group)
	{
		_context.Groups.Add(group);
		_context.SaveChanges();
	}

	public void Update(Group group)
	{
		_context.Groups.Update(group);
		_context.SaveChanges();
	}

	public void Delete(Group group)
	{
		_context.Groups.Remove(group);
		_context.SaveChanges();
	}
}
}