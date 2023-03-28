using System.Collections.Generic;
using System.Linq;
using llama_journal.Data.Repositories;
using llama_journal.Models;

namespace llama_journal.Services;

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

	public List<Grade> GetGradesForUser(int userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
	{
		var user = _userRepository.GetById(userId);
		var discipline = _disciplineRepository.GetById(disciplineId);
		return _disciplineRepository.GetGradesForUserInPeriod(discipline, user, start_datetime, end_datetime);
	}

	public List<Grade> GetGradesForUser(int userId, int disciplineId)
	{
		var user = _userRepository.GetById(userId);
		var discipline = _disciplineRepository.GetById(disciplineId);
		return _gradeRepository.GetGradesForUser(discipline, user);
	}
	public double GetAvarangeGrade(int userId, int disciplineId)
	{
		var user = _userRepository.GetById(userId);
		var discipline = _disciplineRepository.GetById(disciplineId);

		List<Grade> grades = _gradeRepository.GetGradesForGroup(discipline, user.Group);
		double sum = 0;
		foreach (var grade in grades)
			sum += grade.Score;
		return sum / grades.Count;
	}
	public String GetFileWithGrades(int userId, int disciplineId, DateTime start_datetime, DateTime end_datetime)
	{
		var user = _userRepository.GetById(userId);
		var discipline = _disciplineRepository.GetById(disciplineId);

		var grades = _gradeRepository.GetGradesForUserInPeriod(user, start_datetime, end_datetime, discipline);

		// TODO: generate file for grades
		string filename = "";

		return filename;
	}
	public Dictionary<String, String> GetGradesForAllUserDisciplines(int userId, DateTime start_datetime, DateTime end_datetime)
	{
		var user = _userRepository.GetById(userId);
		List<Grade> grades = _gradeRepository.GetGradesForUserInPeriod(user, start_datetime, end_datetime);
		var result = new Dictionary<string, string>();
		foreach (var grade in grades) {
			if (!result.ContainsKey(grade.Discipline.Name))
				result[grade.Discipline.Name] = 0;
			result[grade.Discipline.Name] += grade.Score;
		}
		return result;
	}
	public void AddGrade(int userId, int score, DateTime date)
	{
		_gradeRepository.AddGrade(userId, score, date);
	}
	public void AddGradeComment(int gradeId, string comment)
	{
		var grade = _gradeRepository.GetById(gradeId);
		_gradeRepository.SetGradeComment(grade, comment);
	}
	public Grade GetGradesForGroup(int disciplineId, int groupId, DateTime start_datetime, DateTime end_datetime)
	{
		var discipline = _disciplineRepository.GetById(disciplineId);
		var group = _groupRepository.GetById(groupId);

		return _gradeRepository.GetGradesForGroup(discipline, group);
	}
}
