using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IGroupRepository
{
    List<Group> GetGroups();
    Group? GetById(long id);
    void Add(Group group);
    void Update(Group group);
    void Delete(Group group);
}
}
