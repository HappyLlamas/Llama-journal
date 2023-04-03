using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
public class GradeRepository : IGradeRepository
{
	private readonly ModelsContext _context;

	public GradeRepository(ModelsContext context)
	{
		_context = context;
	}

	public Grade GetById(long id)
	{
		return _context.Set<Grade>().SingleOrDefault(g => g.Id == id);
	}

	public List<Grade> GetGradesForUser(Discipline discipline, User user)
	{
		return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User == user).ToList();
	}

	public List<Grade> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime startDatetime, DateTime endDatetime)
	{
		return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User == user && g.Date >= startDatetime && g.Date <= endDatetime).ToList();
	}

	public List<Grade> GetGradesForGroup(Discipline discipline, Group group)
	{
		return _context.Set<Grade>().Where(g => g.Discipline == discipline && g.User.Group == group).ToList();
	}

	public void AddGrade(string userId, int score, DateTime date)
	{
		var grade = new Grade { User = _context.Set<User>().Find(userId), Score = score, Date = date };
		_context.Set<Grade>().Add(grade);
		_context.SaveChanges();
	}

	public void SetGradeComment(Grade grade, string comment)
	{
		grade.Comment = comment;
		_context.SaveChanges();
	}
}
}
