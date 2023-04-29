using BusinnesLayer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using llama_journal.Models;

namespace llama_journal.Controllers
{
public class LoginController : Controller
{
    private readonly ILoginService _loginService;
	private readonly ILogger _logger;


    public LoginController(ILoginService loginService, ILogger<LoginController> logger)
    {
        _loginService = loginService;
		_logger = logger;
		_logger.LogInformation("In controller init");
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] LoginViewModel model)
    {
        _logger.LogInformation("At index start");
        if (ModelState.IsValid) {
            try {
				_logger.LogInformation("Before service");
                var claimsIdentity = await _loginService.Login(model.Email, model.Password);
                var authProperties = new AuthenticationProperties {
                    IsPersistent = model.RememberMe
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Progress");
            } catch (Exception error) {
				_logger.LogError(error.Message);
                ModelState.AddModelError("Password", "Пароль неправильний");
                ModelState.AddModelError("Email", "Неправильна електрона пошта");
            }

        }

        return View(model);
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View("Signup");
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromForm] SignupViewModel model)
    {
        if (!ModelState.IsValid) {
            return View(model);
        }

        await _loginService.SignUp(model.Email, model.FullName);
        
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }


    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromForm] string email)
    {
        //connect with email
        return View("Index");
    }

    [HttpGet]
    public async Task<IActionResult> ForgotPassword()
    {
        return View("ForgotPassword");
    }
}

}
