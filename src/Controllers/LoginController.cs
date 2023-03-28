using Database;
using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class LoginController: Controller
{
    public IActionResult Index()
    {
        
        return View();
    }

    [HttpPost]
    public IActionResult Check(User user)
    {
        if(!ModelState.IsValid)
            return View("Index");
        return Redirect("Views/Progress/Index.cshtml");
    }
}