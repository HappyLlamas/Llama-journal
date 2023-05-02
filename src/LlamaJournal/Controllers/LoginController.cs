using BusinnesLayer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using llama_journal.Models;
using Microsoft.AspNetCore.Authorization;
using DataLayer.Models;
using LlamaJournal.Models;


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

				return RedirectToAction("Index", "Home");
            } catch (Exception error) {
				_logger.LogError(error.Message);
				TempData["Error"] = error.Message;
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
		if (ModelState.IsValid) {
			try {
				await _loginService.SignUp(model.Email, model.Password, model.ConfirmPassword);
				return View("Index");
			} catch (Exception error) {
				_logger.LogError(error.Message);
			}
		}
        return View(model);
    }


    [HttpGet, Authorize]
    public async Task<IActionResult> Logout()
    {
		_logger.LogInformation("In loggout");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet, Authorize]
    public IActionResult CompleteRegistration()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
		if (role == RoleEnum.Admin.ToString())
			return RedirectToAction("AdminCompleteRegistration");
		return View("CompleteRegistration");
    }

	[HttpGet, Authorize]
    public async Task<IActionResult> AdminCompleteRegistration()
	{
		return View("AdminCompleteRegistration");
	}

	[HttpPost, Authorize]
    public async Task<IActionResult> AdminCompleteRegistration([FromForm] AdminCompleteRegistrationModel model)
    {
		_logger.LogInformation("In complete registration");
		if (ModelState.IsValid) {
			try {
				await _loginService.AdminCompleteRegistration(User.Identity.Name, model.FullName, model.OrganizationName);
				_logger.LogInformation("Admin complete registration");
				return RedirectToAction("Index", "Home");
			} catch (Exception error) {
				_logger.LogError(error.Message);
			}
		}
        return View(model);
    }

	[HttpPost, Authorize]
    public async Task<IActionResult> CompleteRegistration([FromForm] CompleteRegistrationModel model)
    {
		if (ModelState.IsValid) {
			try {
				await _loginService.CompleteRegistration(User.Identity.Name, model.Password, model.ConfirmPassword);
				_logger.LogInformation("User complete registration");
				return RedirectToAction("Index", "Home");
			} catch (Exception error) {
				_logger.LogError(error.Message);
			}
		}
        return View(model);
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
