using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace llama_journal.Controllers
{
    [Authorize(Roles = "Teacher, Admin")]
    public class ItemManagementController : Controller
    {
        private readonly ModelsContext _context;

        public ItemManagementController(ModelsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var disciplines = _context.Disciplines.Include(d => d.Groups).ToList();
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