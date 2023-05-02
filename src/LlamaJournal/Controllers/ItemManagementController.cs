using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinnesLayer.Services;
using LlamaJournal.Models;

namespace llama_journal.Controllers
{
    [Authorize(Roles = "Teacher")]
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
            var userId = User.Identity.Name;
            var user = await _userService.GetUser(userId);
            var disciplines = await _disciplineService.GetAllDisciplines(userId);
            //await _disciplineService.AddGroupToDiscipline(0,0);
            var infoItemCards = new List<InfoItemCard>();
            foreach (var discipline in disciplines)
            {
                infoItemCards.Add(new InfoItemCard(discipline.Id, userId,user.FullName,
                    discipline.Name, user.Group.Name));
            }
            infoItemCards.Add(new InfoItemCard(1,"nooo","Ira", "listening", "PMI-34"));
            return View(infoItemCards);
        }

        [HttpPost]
        public async void AddGrade(int disciplineId, string studentId, int score, string message="")
        {
            var userId = User.Identity.Name;
            await _gradeService.AddGrade(userId, score, DateTime.Now);
        }
        
    }
}
