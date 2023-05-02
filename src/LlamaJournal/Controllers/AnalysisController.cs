using BusinnesLayer.Models;
using BusinnesLayer.Services;
using llama_journal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace llama_journal.Controllers;

[Authorize]
public class AnalysisController: Controller
{
	private readonly IDisciplineService _disciplineService;
    private readonly IAnalysisService _analysisService;
	private readonly ILogger _logger;

    public AnalysisController(IAnalysisService analysisService, ILogger<AnalysisController> logger, 
	    IDisciplineService disciplineService)
    {
        _analysisService = analysisService;
		_logger = logger;
		_disciplineService = disciplineService;
    }

    [HttpGet]
	public async Task<IActionResult> Index(string selectedValue="") 
    {
		_logger.LogInformation("Index page");
        var users = await _analysisService.GetUsersForGroup(User.Identity.Name);

		_logger.LogInformation("User group: " + users[0].Group.Name);
		var result = new List<AnalysisUserModel>(users.Count);
		var analysis = await _analysisService.GetAnalysis(User.Identity.Name);



		foreach (var user in users)
		{
			if (selectedValue == "average")
				result.Add(new AnalysisUserModel(user, analysis.AverageScore, selectedValue));
			else if (selectedValue == "successful")
				result.Add(new AnalysisUserModel(user, analysis.PassingGrades, selectedValue));
			else if (selectedValue == "unsuccessful")
				result.Add(new AnalysisUserModel(user, analysis.FailingGrades, selectedValue));
			else
				result.Add(new AnalysisUserModel(user, -1, selectedValue));
			}

	
		_logger.LogInformation("Result count: " + result.Count.ToString());

        return this.View(result);
    }
}

