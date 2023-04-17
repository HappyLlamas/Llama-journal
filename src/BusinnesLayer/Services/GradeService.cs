using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public class GradesService: IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDisciplineRepository _disciplineRepository;
    private readonly IGroupRepository _groupRepository;

    public GradesService(IGradeRepository gradeRepository, IUserRepository userRepository, IDisciplineRepository disciplineRipository, IGroupRepository groupRepository)
    {
        _gradeRepository = gradeRepository;
        _userRepository = userRepository;
        _disciplineRepository = disciplineRipository;
        _groupRepository = groupRepository;
    }

    public async Task<List<Grade>> GetGradesForUser(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
    {
        var user = await _userRepository.GetById(userId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        return await _disciplineRepository.GetGradesForUserInPeriod(discipline, userId, start_datetime, end_datetime);
    }

    public async Task<List<Grade>> GetGradesForUser(string userId, int disciplineId)
    {
        var user = await _userRepository.GetById(userId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        return await _gradeRepository.GetGradesForUser(discipline, user);
    }
    public async Task<double> GetAvarangeGrade(string userId, int disciplineId)
    {
        var user = await _userRepository.GetById(userId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        List<Grade> grades = await _gradeRepository.GetGradesForGroup(discipline, user.Group);
        double sum = 0;
        foreach (var grade in grades)
            sum += grade.Score;
        return sum / grades.Count;
    }
    public async Task<string> GetFileWithGrades(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
    {
        var user = await _userRepository.GetById(userId);
        var discipline = await _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        var grades = await _gradeRepository.GetGradesForUserInPeriod(discipline, user, start_datetime, end_datetime);

        // TODO: generate file for grades
        string filename = "";

        return filename;
    }
    public async Task<Dictionary<string, string>> GetGradesForAllUserDisciplines(string userId, DateTime startDatetime, DateTime endDatetime)
    {
        var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        var disciplines = await _disciplineRepository.GetAll();

        var result = new Dictionary<string, string>();
        foreach (var discipline in disciplines) {
            List<Grade> grades = await _gradeRepository.GetGradesForUserInPeriod(discipline, user, startDatetime, endDatetime);
            if (grades.Any()) {
                double average = grades.Average(g => g.Score);
                result.Add(discipline.Name, average.ToString());
            }
        }

        return result;
    }
    public async Task AddGrade(string userId, int score, DateTime date)
    {
		var user = await _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        await _gradeRepository.AddGrade(user, score, date);
    }
    public async Task AddGradeComment(int gradeId, string comment)
    {
        var grade = await _gradeRepository.GetById(gradeId);

		if(grade == null)
			throw new Exception($"Grade with id {gradeId} not found");

		grade.Comment = comment;
		await _gradeRepository.Update(grade);
    }
    public async Task<List<Grade>> GetGradesForGroup(int disciplineId, int groupId, DateTime start_datetime, DateTime end_datetime)
    {
        var discipline = await _disciplineRepository.GetById(disciplineId);
        var group = await _groupRepository.GetById(groupId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(group == null)
			throw new Exception($"Group with id {groupId} not found");

        return await _gradeRepository.GetGradesForGroup(discipline, group);
    }
}
