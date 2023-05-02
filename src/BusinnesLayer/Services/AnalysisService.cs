using DataLayer.Repositories;
using DataLayer.Models;
using BusinnesLayer.Models;

namespace BusinnesLayer.Services;

public class AnalysisService: IAnalysisService
{
	private readonly IGradeRepository _gradeRepository;
	private readonly IGroupRepository _groupRepository;
	private readonly IUserRepository _userRepository;
	private readonly IDisciplineRepository _disciplineRepository;

	public AnalysisService(IGradeRepository gradeRepository, IGroupRepository groupRepository, IUserRepository userRepository, IDisciplineRepository disciplineRepository)
	{
		_gradeRepository = gradeRepository;
		_groupRepository = groupRepository;
		_userRepository = userRepository;
		_disciplineRepository = disciplineRepository;
	}
    public async Task<AnalysisModel> GetAnalysis(string userId)
    {
		var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");
		
		
		var disciplines = await _disciplineRepository.GetDisciplines(user: user);
		var gradesDetailModels = new AnalysisModel();
		foreach (var discipline in disciplines)
		{
			var grades = await _gradeRepository.GetGradesForUser(discipline, user);
			gradesDetailModels.AverageScore += grades.Sum(g => g.Score);

			if (grades.Sum(g => g.Score) >= 51)
				gradesDetailModels.PassingGrades++;
			else
				gradesDetailModels.FailingGrades++;
		}

		gradesDetailModels.AverageScore /= disciplines.Count;

		return gradesDetailModels;
    }
	public async Task<List<User>> GetUsersForGroup(string userId)
	{
		var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

		return await _groupRepository.GetUsersInGroup(user.Group);	
	}

}
