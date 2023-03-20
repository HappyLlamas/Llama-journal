using System.Collections.Generic;
using System.Linq;
using llama_journal.Data.Repositories;
using llama_journal.Models;

namespace llama_journal.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers().ToList();
        }
    }
}