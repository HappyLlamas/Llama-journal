using System.Collections.Generic;
using llama_journal.Models;

namespace llama_journal.Data.Repositories
{
    public interface IGroupRepository
    {
        List<Group> GetGroups();
        Group GetById(long id);
        void Add(Group group);
        void Update(Group group);
        void Delete(Group group);
    }
}