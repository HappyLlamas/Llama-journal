// <copyright file="AnalysisController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using LlamaJournal.Models;

namespace LlamaJournal.Controllers
{
    using BusinnesLayer.Services;

    /// <inheritdoc />
    [Authorize]
    public class AnalysisController : Controller
    {
        private readonly IAnalysisService _analysisService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisController"/> class.
        /// </summary>
        /// <param name="analysisService"> analysisService. </param>
        /// <param name="logger"> logger. </param>
        public AnalysisController(IAnalysisService analysisService, ILogger<AnalysisController> logger)
        {
            this._analysisService = analysisService;
            this._logger = logger;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            this._logger.LogInformation("Index page");
            var users = await this._analysisService.GetUsersForGroup(this.User.Identity!.Name ?? string.Empty);

            this._logger.LogInformation("User group: " + users[0].Group.Name);
            var result = new List<AnalysisUserModel>(users.Count);

            foreach (var user in users)
            {
                result.Add(new AnalysisUserModel(user));
            }

            this._logger.LogInformation("Result count: " + result.Count.ToString());

            return this.View(result);
        }
    }
}