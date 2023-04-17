using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

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

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        _userRepository.SetGroup(user, groupName);
    }

    public void SetUserRole(string userId, RoleEnum role)
    {
        var user = _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        _userRepository.SetRole(user, role);
    }
    public void CreateUser(string email, string fullname)
    {
        _userRepository.CreateUser(email, fullname);
	}
}

