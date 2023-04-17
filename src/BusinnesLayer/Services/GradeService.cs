using DataLayer.Repositories;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public class GradesService
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

    public List<Grade> GetGradesForUser(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
    {
        var user = _userRepository.GetById(userId);
        var discipline = _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        return _disciplineRepository.GetGradesForUserInPeriod(discipline, userId, start_datetime, end_datetime);
    }

    public List<Grade> GetGradesForUser(string userId, int disciplineId)
    {
        var user = _userRepository.GetById(userId);
        var discipline = _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        return _gradeRepository.GetGradesForUser(discipline, user);
    }
    public double GetAvarangeGrade(string userId, int disciplineId)
    {
        var user = _userRepository.GetById(userId);
        var discipline = _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        List<Grade> grades = _gradeRepository.GetGradesForGroup(discipline, user.Group);
        double sum = 0;
        foreach (var grade in grades)
            sum += grade.Score;
        return sum / grades.Count;
    }
    public String GetFileWithGrades(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
    {
        var user = _userRepository.GetById(userId);
        var discipline = _disciplineRepository.GetById(disciplineId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        var grades = _gradeRepository.GetGradesForUserInPeriod(discipline, user, start_datetime, end_datetime);

        // TODO: generate file for grades
        string filename = "";

        return filename;
    }
    public Dictionary<string, string> GetGradesForAllUserDisciplines(string userId, DateTime startDatetime, DateTime endDatetime)
    {
        var user = _userRepository.GetById(userId);

		if(user == null)
			throw new Exception($"User with id {userId} not found");

        var disciplines = _disciplineRepository.GetAll();

        var result = new Dictionary<string, string>();
        foreach (var discipline in disciplines) {
            List<Grade> grades = _gradeRepository.GetGradesForUserInPeriod(discipline, user, startDatetime, endDatetime);
            if (grades.Any()) {
                double average = grades.Average(g => g.Score);
                result.Add(discipline.Name, average.ToString());
            }
        }

        return result;
    }
    public void AddGrade(string userId, int score, DateTime date)
    {
        _gradeRepository.AddGrade(userId, score, date);
    }
    public void AddGradeComment(int gradeId, string comment)
    {
        var grade = _gradeRepository.GetById(gradeId);

		if(grade == null)
			throw new Exception($"Grade with id {gradeId} not found");

        _gradeRepository.SetGradeComment(grade, comment);
    }
    public List<Grade> GetGradesForGroup(int disciplineId, int groupId, DateTime start_datetime, DateTime end_datetime)
    {
        var discipline = _disciplineRepository.GetById(disciplineId);
        var group = _groupRepository.GetById(groupId);

		if(discipline == null)
			throw new Exception($"Discipline with id {disciplineId} not found");

		if(group == null)
			throw new Exception($"Group with id {groupId} not found");

        return _gradeRepository.GetGradesForGroup(discipline, group);
    }
}
