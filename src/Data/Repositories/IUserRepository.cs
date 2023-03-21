using System.Collections.Generic;
using llama_journal.Models;

namespace llama_journal.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}