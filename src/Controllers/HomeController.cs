using Microsoft.AspNetCore.Mvc;

namespace llama_journal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();;
        }
        public IActionResult About()
        {
            return View();
        }
    }
}