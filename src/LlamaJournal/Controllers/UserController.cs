using BusinnesLayer.Services;
using DataLayer.Models;
using LlamaJournal.Models;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserService _userService;
        List<UserCard> actions = new List<UserCard>()
        {
            new UserCard("User","Create"),
            new UserCard("User","Edit"),
            new UserCard("User","Delete"),
        };

        public UserAccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            
            var user = await _userService.GetUser(User.Identity.Name);
            
            if(!user.CompleteRegistration)
                return RedirectToAction("CompleteRegistration", "Login");
            
            if(user.Role != RoleEnum.Admin)
                return RedirectToAction("Index", "Login");

            return View(actions);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string id, string email, string fullname, string role, string password, string group)
        {
            
            var user = new User{Id = id, Email = email, FullName = fullname,
                Role = Enum.Parse<RoleEnum>(role), Password = password};
            await _userService.CreateUser(user);
            
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string fullname, string role, string group)
        {
            var user = await _userService.GetUser(id);
            await _userService.EditUser(user);

            if (!ModelState.IsValid)
            {
                // тут відбуваються зміна даних
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetUser(id);
            await _userService.DeleteUser(user);
            return View();
        }

    }
}