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
    public async Task<GradesDetailModel> GetAnalysis(string userId, int disciplineId)
    {
		var user = await _userRepository.GetById(userId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        var grades = await _gradeRepository.GetGradesForUser(discipline, user);

		return new GradesDetailModel{
			averageScore=grades.Average(g => g.Score),
			maxScore=grades.Max(g => g.Score),
			minScore=grades.Min(g => g.Score),
			numGrades=grades.Count,
		};
    }
	public async Task<List<User>> GetUsersForGroup(string userId)
	{
		var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

		return await _groupRepository.GetUsersInGroup(user.Group);	
	}

}
