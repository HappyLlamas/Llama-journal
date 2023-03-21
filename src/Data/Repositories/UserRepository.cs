using System.Collections.Generic;
using System.Linq;
using llama_journal.Models;

namespace llama_journal.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>()
        {
            new User { Id = 1, Name = "John Doe", Age = 25 },
            new User { Id = 2, Name = "Jane Smith", Age = 30 }
        };

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}