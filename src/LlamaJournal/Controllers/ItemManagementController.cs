using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using DataLayer.Repositories;

namespace llama_journal.Controllers
{
    [Authorize(Roles = "Teacher, Admin")]
    public class ItemManagementController : Controller
    {
        private readonly ModelsContext _context;
        private readonly IUserRepository _userRepository;

        public ItemManagementController(ModelsContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var disciplines = _context.Disciplines.Include(d => d.Groups).ToList();
            var user = _userRepository.FindByEmail(User.Identity.Name);
            return View(disciplines);
        }

        public IActionResult EditGrade(long id)
        {
            var grade = _context.Grades.Include(g => g.User).Include(g => g.Discipline).FirstOrDefault(g => g.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        [HttpPost]
        public IActionResult EditGrade(long id, Grade model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var grade = _context.Grades.FirstOrDefault(g => g.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            grade.Score = model.Score;
            grade.Comment = model.Comment;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }

        private bool GradeExists(long id)
        {
            return _context.Grades.Any(e => e.Id == id);
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