using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinnesLayer.Services;

namespace llama_journal.Controllers
{
    [Authorize(Roles = "Teacher, Admin")]
    public class ItemManagementController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDisciplineService _disciplineService;
        private readonly IGradeService _gradeService;

        public ItemManagementController(IUserService userService, IDisciplineService disciplineService, IGradeService gradeService)
        {
            _userService = userService;
			_disciplineService = disciplineService;
			_gradeService = gradeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _disciplineService.GetAllDisciplines());
        }


        public async Task<IActionResult> EditGrade(long id)
        {
			try {
				return View(await _gradeService.GetGrade(id));
			}
			catch (Exception error) {
				return NotFound();
			}
        }

        [HttpPost]
        public async Task<IActionResult> EditGrade(long id, Grade model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
			try {
				await _gradeService.EditGrade(id, model.Score, model.Comment);
				return RedirectToAction("Index");
			}
			catch (Exception error) {
				return NotFound();
			}
        }
    }
}

public class InfoItemCard
{
    public string Subject { get; set; }

    public List<string> FullNameTeachers { get; set; }

    public List<int> Grades { get; set; }

    public InfoItemCard(List<string> fullNameTeachers, List<int> grades, string subject)
    {
        FullNameTeachers = fullNameTeachers;
        Grades = grades;
        Subject = subject;
    }
}
//
// [HttpGet]
// public IActionResult DescribeMarks()
// {
//     return View();
// }
