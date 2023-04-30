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
            // дисципліни, які веде викладач
            var userId = "myId";
            var disciplines = await _disciplineService.GetAllDisciplines(userId);
            var infoItemCards = new List<InfoItemCard>
            {
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
            };
            return View(infoItemCards);
        }


        public async void AddGrade(int id)
        {
            // добавлення оцінки у бд для конкрентого юзера
        }
        
    }
}


//
// [HttpGet]
// public IActionResult DescribeMarks()
// {
//     return View();
// }
