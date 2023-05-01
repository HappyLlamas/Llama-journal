using BusinnesLayer.Services;
using llama_journal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace llama_journal.Controllers;

[Authorize]
public class AnalysisController: Controller
{
    private readonly IAnalysisService _analysisService;
	private readonly ILogger _logger;

    public AnalysisController(IAnalysisService analysisService, ILogger<AnalysisController> logger)
    {
        _analysisService = analysisService;
		_logger = logger;
    }

    [HttpGet]
	public async Task<IActionResult> Index()
    {
		_logger.LogInformation("Index page");
        var users = await _analysisService.GetUsersForGroup(User.Identity.Name);

		_logger.LogInformation("User group: " + users[0].Group.Name);
		var result = new List<AnalysisUserModel>(users.Count);

		foreach(var user in users)
			result.Add(new AnalysisUserModel(user));
	
		_logger.LogInformation("Result count: " + result.Count.ToString());

        return this.View(result);
    }
}

