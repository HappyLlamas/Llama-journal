// <copyright file="ProgressController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using LlamaJournal.Models;

namespace LlamaJournal.Controllers
{
    using BusinnesLayer.Services;

    /// <inheritdoc />
    [Authorize]
    public class ProgressController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressController"/> class.
        /// </summary>
        /// <param name="gradeService"> grade. </param>
        /// <param name="logger"> logger. </param>
        public ProgressController(IGradeService gradeService, ILogger<ProgressController> logger)
        {
            this._gradeService = gradeService;
            this._logger = logger;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <param name="model"> model. </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            this._logger.LogInformation("Logged in user id: " + this.User.Identity!.Name);

            var grades = await this._gradeService.GetGradesForUser(this.User.Identity!.Name ?? string.Empty);
            var cards = new List<Card>();
            foreach (var grade in grades)
            {
                cards.Add(new Card(
                    grade.disciplineName,
                    grade.teacherName,
                    grade.totalGrades));
            }

            this._logger.LogInformation("Count of cards in ProgressIndex: " + cards.Count.ToString());
            return this.View(cards);
        }

    }
}
