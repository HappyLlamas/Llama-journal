using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class ItemManagementController:Controller
{
    public IActionResult Index()
    {
        return View();;
    }
}