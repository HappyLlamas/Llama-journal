using System.Collections.Generic;
using System.Linq;
using llama_journal.Data.Repositories;
using llama_journal.Models;

namespace llama_journal.Services;

public class DisciplineService
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

	public List<Discipline> GetAllDisciplines()
	{
		return _disciplineRepository.GetDisciplines();
	}
	public void AddGroupToDiscipline(int disciplineId, int groupId)
	{
		var group = _groupRepository.GetById(groupId);
		var discipline = _disciplineRepository.GetById(disciplineId);
		_disciplineRepository.AddGroupToDiscipline(discipline, group);
	}
	public bool ChangeDisciplineDescription(int disciplineId, string description)
	{
		var discipline = _disciplineRepository.GetById(disciplineId);

		discipline.Description = description;

		_disciplineRepository.Update(discipline);
		return true;
	}
}
