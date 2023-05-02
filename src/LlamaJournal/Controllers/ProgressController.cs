using BusinnesLayer.Services;
using llama_journal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace llama_journal.Controllers
{

	[Authorize]
    public class ProgressController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;
		private readonly ILogger _logger;

        public ProgressController(IGradeService gradeService, IUserService userService, ILogger<ProgressController> logger)
        {
            _gradeService = gradeService;
            _userService = userService;
			_logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string chooseOption)
        {
			_logger.LogInformation("Logged in user id: " + User.Identity.Name);

			var grades = await _gradeService.GetGradesForUser(User.Identity.Name);
            var cards = new List<Card>();

            switch (chooseOption)
            {
                case "math":
                    grades = grades.Where(g => g.disciplineName == chooseOption).ToList();
                    break;
                case "computerEngineering":
                    grades = grades.Where(g => g.disciplineName == chooseOption).ToList();
                    break;
                case "optimizationMmethod":
                    grades = grades.Where(g => g.disciplineName == chooseOption).ToList();
                    break;
            }
            
            foreach(var grade in grades) {
                cards.Add(new Card(
                    grade.disciplineName,
                    grade.teacherName,
                    grade.totalGrades
				));
			}
			_logger.LogInformation("Count of cards in ProgressIndex: " + cards.Count.ToString());

            return View(cards);
        }


        public class GradesViewModel
        {
            public int disciplineId;
        }
    }

}

