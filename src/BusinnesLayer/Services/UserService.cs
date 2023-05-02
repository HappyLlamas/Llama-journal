using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
		_groupRepository = groupRepository;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }

    public async Task<User> GetUser(string userId)
    {
        var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

		return user;
    }
    public async Task SetUserGroup(string userId, int groupId)
    {
        var user = await _userRepository.GetById(userId);
        var group = await _groupRepository.GetById(groupId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

		if(group == null)
			throw new Exception($"Group with id {groupId} not found");

		user.Group = group;

		await _userRepository.Update(user);
    }

    public async Task SetUserRole(string userId, RoleEnum role)
    {
        var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

		user.Role = role;

        await _userRepository.Update(user);
    }
    public async Task CreateUser(User user)
    {
        await _userRepository.CreateUser(user);
	}
}

