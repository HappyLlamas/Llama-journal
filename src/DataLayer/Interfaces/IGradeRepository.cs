using DataLayer.Models;

namespace DataLayer.Repositories
{
public interface IGradeRepository
{
    Task<Grade?> GetById(long id);
    Task<List<Grade>> GetGradesForUser(Discipline discipline, User user);
    Task<List<Grade>> GetGradesForUserInPeriod(Discipline discipline, User user, DateTime startDatetime, DateTime endDatetime);
    Task<List<Grade>> GetGradesForGroup(Discipline discipline, Group group);
    Task AddGrade(User user, int score, DateTime date);
	Task Update(Grade grade);
}
}
