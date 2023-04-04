using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class UserAccountController: Controller
{
    public IActionResult Index()
    {
        return View();
    }
}