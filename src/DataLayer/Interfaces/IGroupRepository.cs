using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IGroupRepository
{
    Task<List<Group>> GetGroups();
    Task<Group?> GetById(long id);
    Task Add(Group group);
    Task Update(Group group);
    Task Delete(Group group);
}
}
