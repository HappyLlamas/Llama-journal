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
		var result = _context.Groups.FirstOrDefault(g => g.Id == id);
		if(result == null)
			throw new Exception($"Group with id {id} not found");
        return result;
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
