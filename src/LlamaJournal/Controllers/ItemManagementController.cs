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
            var user_2 = disciplines[0];
            List<InfoItemCard> listInfoItemCard = new List<InfoItemCard>();
            for (int i = 0; i < user.Grades.Count; i++)
            {
                listInfoItemCard.Add(new InfoItemCard(disciplines[i].Teachers.ToList()[0], user.Grades, disciplines[i].Name));
            }
            return View(listInfoItemCard);
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


//
// [HttpGet]
// public IActionResult DescribeMarks()
// {
//     return View();
// }