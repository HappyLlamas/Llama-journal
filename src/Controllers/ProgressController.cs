using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class ProgressController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
}