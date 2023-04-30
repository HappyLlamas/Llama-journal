using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinnesLayer.Services;
using DataLayer.Models;

namespace llama_journal.Controllers
{
    public class HomeController : Controller
    {
		private readonly IUserService _userService;

		public HomeController(IUserService userService)
		{
			_userService = userService;
		}
		[Authorize]
        public async Task<IActionResult> Index()
        {
			var user = await _userService.GetUser(User.Identity.Name);
			if(!user.CompleteRegistration)
				return RedirectToAction("CompleteRegistration", "Login");

			switch(user.Role)
			{
				case RoleEnum.Admin:
					return RedirectToAction("Index", "Progress");
				case RoleEnum.Teacher:
					return RedirectToAction("Index", "Progress");
				case RoleEnum.User:
					return RedirectToAction("Index", "Progress");
			}
            return View();
        }
    }
}
