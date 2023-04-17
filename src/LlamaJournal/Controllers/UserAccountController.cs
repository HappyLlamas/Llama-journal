using BusinnesLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUser(id);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        // public IActionResult Edit(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string id, User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid)
        //     {
        //         _userRepository.SetGroup(user, user.Group.Name);
        //         _userRepository.SetRole(user, user.Role);
        //         _userRepository.SetUserPassword(user, user.Password);
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(user);
        // }
        //
        // public IActionResult Delete(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }
        //
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public IActionResult DeleteConfirmed(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _userRepository.DeleteUser(user);
        //     return RedirectToAction(nameof(Index));
        // }
    }
}
