using BusinnesLayer.Services;
using llama_journal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    //[Authorize]
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
        public async Task<IActionResult> Index(GradesViewModel model)
        {
			var grades = await _gradeService.GetGradesForUser("1111");
			var cards = new List<Card>();
			foreach(var grade in grades)
				cards.Add(new Card(
					grade.disciplineName,
					grade.teacherName,
					grade.grades
				));
            return View(cards);
        }


        public class GradesViewModel
        {
            public int disciplineId;
        }
    }

}

