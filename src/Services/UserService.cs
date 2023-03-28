using System.Collections.Generic;
using System.Linq;
using llama_journal.Data.Repositories;
using llama_journal.Models;

namespace llama_journal.Services;

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
	public void SetUserGroup(string userId, string groupName)
    {
        var user = _userRepository.GetById(userId);
        _userRepository.SetGroup(user, groupName);
    }

	public void SetUserRole(string userId, RoleEnum role)
    {
        var user = _userRepository.GetById(userId);
        _userRepository.SetRole(user, role);
    }
	public void CreateUser(string email, string fullname)
	{
		_userRepository.CreateUser(email, fullname);
	}
}

