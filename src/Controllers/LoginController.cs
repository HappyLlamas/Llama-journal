using Database;
using llama_journal.Models;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class LoginController: Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index([FromForm]LoginViewModel model)
    {
        if (this.ModelState.IsValid)
        {
            if (model.Id == "123456" && model.Email == "example@gmail.com" && model.Password == "password")
            {
                return this.RedirectToAction("Index", "Progress");
            }
            else
            {
                this.ModelState.AddModelError("", "Invalid login credentials");
            }
        }

        return this.View(model);
    }
}