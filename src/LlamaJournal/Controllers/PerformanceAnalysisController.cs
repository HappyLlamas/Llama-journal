using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers;

public class PerformanceAnalysisController: Controller
{
    public IActionResult Index()
    {
        return View();;
    }
}