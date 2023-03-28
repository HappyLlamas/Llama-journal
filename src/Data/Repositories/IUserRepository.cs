using System.Collections.Generic;
using llama_journal.Models;

namespace llama_journal.Data.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetById(string userId);
        User FindByEmail(string email);
        void SetGroup(User user, string groupName);
        void SetRole(User user, RoleEnum role);
        void CreateUser(string email, string fullname);
        void SetUserPassword(User user, string password);
        bool CheckPassword(User user, string password);
    }
}