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

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index([FromForm] LoginViewModel model)
    {
        if (ModelState.IsValid) {
            try {
                var claimsIdentity = await _loginService.Login(model.Email, model.Password);
                var authProperties = new AuthenticationProperties {
                    IsPersistent = model.RememberMe
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Progress");
            } catch (InvalidDataException error) {
                this.ModelState.AddModelError(" ", error.Message);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp([FromForm] SignupViewModel model)
    {
        if (ModelState.IsValid) {
            // var existingUser = _userRepository.FindByEmail(model.Email);
            // if (existingUser != null)
            // {
            //     ModelState.AddModelError("", "A user with this email already exists.");
            //     return View(model);
            // }
            // _userRepository.CreateUser(model.Email, model.FullName);
            // return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
}

}
