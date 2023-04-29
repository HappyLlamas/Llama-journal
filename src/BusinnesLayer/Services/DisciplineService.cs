using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public class DisciplineService: IDisciplineService
{
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;

    public DisciplineService(IDisciplineRepository disciplineRepository, IUserRepository userRepository, IGroupRepository groupRepository)
    {
        _disciplineRepository = disciplineRepository;
        _userRepository = userRepository;
        _groupRepository = groupRepository;
    }

    public async Task<List<Discipline>> GetAllDisciplines(string userId)
    {
		var user = await _userRepository.GetById(userId);
		if(user == null)
			throw new Exception($"User with id {userId} not found");

        return await _disciplineRepository.GetDisciplines(user: user);
    }
    public async Task AddGroupToDiscipline(int disciplineId, int groupId)
    {
        var group = await _groupRepository.GetById(groupId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(group == null)
			throw new Exception($"Group with id {groupId} not found");

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		discipline.Groups.Add(group);
		await _disciplineRepository.Update(discipline);
    }
    public async Task ChangeDisciplineDescription(int disciplineId, string description)
    {
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

        discipline.Description = description;
        await _disciplineRepository.Update(discipline);
    }
}
