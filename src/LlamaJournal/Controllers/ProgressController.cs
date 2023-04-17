using DataLayer.Repositories;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace llama_journal.Controllers
{
    //[Authorize]
    public class ProgressController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IUserRepository _userRepository;
        public IList<Card> cards = new List<Card> { };

        public ProgressController(IGradeRepository gradeRepository, IUserRepository userRepository)
        {
            _gradeRepository = gradeRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index(int pageIndex = 1)
        {
            var user = _userRepository.FindByEmail(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            // Get grades for the user's first discipline in their group
            var discipline = user.Group.Disciplines.FirstOrDefault();
            var disciplines = user.Group.Disciplines.ToList();
            var grades = _gradeRepository.GetGradesForUser(discipline, user);
            for (int i = 0; i < disciplines.Count; i++)
            {
                this.cards.Add(new Card(disciplines[i].Name, discipline.Teachers[0].FullName, grades));
            }

            // Check if the user has access to grades for other users in the group
            var canAccessAllGrades = User.IsInRole(RoleEnum.Teacher.ToString())
                                     || User.IsInRole(RoleEnum.Admin.ToString());

            // Calculate analytical fields
            var averageScore = grades.Count > 0 ? grades.Average(g => g.Score) : 0;
            var maxScore = grades.Count > 0 ? grades.Max(g => g.Score) : 0;
            var minScore = grades.Count > 0 ? grades.Min(g => g.Score) : 0;
            var numGrades = grades.Count;
            var passingGrades = grades.Count(g => g.Score >= 60);
            var failingGrades = grades.Count(g => g.Score < 60);
            var passingGradePercentage = numGrades > 0 ? ((double)passingGrades / numGrades) * 100 : 0;
            var failingGradePercentage = numGrades > 0 ? ((double)failingGrades / numGrades) * 100 : 0;

            // Create view model and pass to view
            var viewModel = new ProgressViewModel
            {
                User = user,
                Grades = canAccessAllGrades ? grades : null,
                AverageScore = averageScore,
                MaxScore = maxScore,
                MinScore = minScore,
                NumGrades = numGrades,
                PassingGrades = passingGrades,
                FailingGrades = failingGrades,
                PassingGradePercentage = passingGradePercentage,
                FailingGradePercentage = failingGradePercentage
            };
            
            var resultCards = cards.OrderBy(c => c.Subject)
                .Skip((pageIndex - 1) * 4)
                .Take(4)
                .ToList();
            
            return View(resultCards);
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
        public List<Grade> Grades { get; set; }

        public Card(string subject, string fullName, List<Grade> grades)
        {
            Subject = subject;
            FullName = fullName;
            Grades = grades;
        }
    }
    
    
}
