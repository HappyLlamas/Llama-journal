using BusinnesLayer.Models;
using DataLayer.Models;

namespace BusinnesLayer.Services;

public interface IAnalysisService
{
	Task<AnalysisModel> GetAnalysis(string userId);
	Task<List<User>> GetUsersForGroup(string userId);
}
