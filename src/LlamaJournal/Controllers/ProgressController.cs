using BusinnesLayer.Services;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserService _userService;

        public ProgressController(IGradeService gradeService, IUserService userService)
        {
            _gradeService = gradeService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(GradesViewModel model)
        {
            List<Card> cards = new List<Card>()
            {
                new Card("math", "Zubenko Muhail Petrovych", new List<int>() { 3, 5, 3, 6 }),
                new Card("math", "Zubenko Muhail Petrovych", new List<int>() { 3, 5, 3, 6 }),
                new Card("math", "Zubenko Muhail Petrovych", new List<int>() { 3, 5, 3, 6 }),
                new Card("math", "Zubenko Muhail Petrovych", new List<int>() { 3, 5, 3, 6 }),
            };
            return View(cards);
        }


        public class GradesViewModel
        {
            public int disciplineId;
        }
    }
    
    public class ProgressViewModel
    {
        public User User { get; set; }
        public List<Grade> Grades { get; set; }
        public double AverageScore { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }
        public int NumGrades { get; set; }
        public int PassingGrades { get; set; }
        public int FailingGrades { get; set; }
        public double PassingGradePercentage { get; set; }
        public double FailingGradePercentage { get; set; }
        public string Subject { get; set; }
        public string FullName { get; set; }
    }
    public class Card
    {
        public string Subject { get; set; }
        public string FullName { get; set; }
        public List<int> Grades { get; set; }

        public Card(string subject, string fullName, List<int> gradesUrl)
        {
            Subject = subject;
            FullName = fullName;
            Grades = gradesUrl;
        }
    }
}

