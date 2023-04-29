using DataLayer.Models;
using BusinnesLayer.Models;

namespace BusinnesLayer.Services;

public interface IGradeService
{
	Task<Grade> GetGrade(long gradeId);
	Task EditGrade(long gradeId, int score, string? comment);
    Task<List<GradesPerDiscipline>> GetGradesForUser(string userId, DateTime? start_datetime=null, DateTime? end_datetime=null);
    Task<GradesDetailModel> GetGradesDetail(string userId, int disciplineId);
    Task<string> GetFileWithGrades(string userId, int disciplineId, DateTime start_datetime, DateTime end_datetime);
    Task<Dictionary<string, string>> GetGradesForAllUserDisciplines(string userId, DateTime startDatetime, DateTime endDatetime);
    Task<List<Grade>> GetGradesForGroup(int disciplineId, int groupId, DateTime start_datetime, DateTime end_datetime);
}
