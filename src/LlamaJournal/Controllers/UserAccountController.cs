using BusinnesLayer.Services;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserService _userService;

        public UserAccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var actions = new List<UserCard>()
            {
                new UserCard("User","Create"),
                new UserCard("User","Edit"),
                new UserCard("User","Delete"),
            };
            return View(actions);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string email, string fullname, string role, string password, string group)
        {
            // var user = new User(Guid.NewGuid().ToString(), email, fullname, role, password, group);
            // _userService.CreateUser(user);
            
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
            var user = _userService.GetUser(id);

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
        public IActionResult Delete(string id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }

    }
    
    public class UserCard{
        public string Action { get; set; }
        public string Member { get; set; }
        
        public UserCard(string member, string action)
        {
            Action = action;
            Member = member;
        }
    }
}


        

