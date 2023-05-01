using BusinnesLayer.Models;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IAnalysisService
{
	Task<GradesDetailModel> GetAnalysis(string userId, int disciplineId);
	Task<List<User>> GetUsersForGroup(string userId);
}
