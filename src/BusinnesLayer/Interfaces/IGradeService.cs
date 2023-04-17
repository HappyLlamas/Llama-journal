using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IGradeService
{
    Task<List<Grade>> GetGradesForUser(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime);
    Task<List<Grade>> GetGradesForUser(string userId, int disciplineId);
    Task<double> GetAvarangeGrade(string userId, int disciplineId);
    Task<string> GetFileWithGrades(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime);
    Task<Dictionary<string, string>> GetGradesForAllUserDisciplines(string userId, DateTime startDatetime, DateTime endDatetime);
    Task AddGrade(string userId, int score, DateTime date);
    Task AddGradeComment(int gradeId, string comment);
    Task<List<Grade>> GetGradesForGroup(int disciplineId, int groupId, DateTime start_datetime, DateTime end_datetime);
}
