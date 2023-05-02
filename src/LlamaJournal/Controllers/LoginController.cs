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

	/// <summary>
	/// Initializes a new instance of the <see cref="LoginController"/> class.
	/// </summary>
	/// <param name="loginService"> loginService. </param>
	/// <param name="logger"> logger. </param>
	public LoginController(ILoginService loginService, ILogger<LoginController> logger)
	{
		this._loginService = loginService;
		this._logger = logger;
	}

	public IActionResult Index()
	{
		return this.View();
	}

	/// <summary>
	/// Index.
	/// </summary>
	/// <param name="model"> model. </param>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpPost]
	public async Task<IActionResult> Index([FromForm] LoginViewModel model)
	{
		this._logger.LogInformation("At index start");
		if (this.ModelState.IsValid)
		{
			try
			{
				this._logger.LogInformation("Before service");
				var claimsIdentity = await this._loginService.Login(model.Email, model.Password);
				var authProperties = new AuthenticationProperties
				{
					IsPersistent = model.RememberMe,
				};
				await this.HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
					authProperties);

				return this.RedirectToAction("Index", "Home");
			}
			catch (Exception error)
			{
				this._logger.LogError(error.Message);
				this.TempData["Error"] = error.Message;
			}
		}

		return this.View(model);
	}

	[HttpGet]
	public IActionResult SignUp()
	{
		return this.View("Signup");
	}

	/// <summary>
	/// Signup.
	/// </summary>
	/// <param name="model"> model. </param>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpPost]
	public async Task<IActionResult> SignUp([FromForm] SignupViewModel model)
	{
		if (this.ModelState.IsValid)
		{
			try
			{
				await this._loginService.SignUp(model.Email, model.Password, model.ConfirmPassword);
				return this.View("Index");
			}
			catch (Exception error)
			{
				this._logger.LogError(error.Message);
				this.TempData["Error"] = error.Message;
			}
		}

		return this.RedirectToAction("Index", "Home");
	}

	/// <summary>
	/// Logout.
	/// </summary>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Logout()
	{
		this._logger.LogInformation("In logout");
		await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return this.RedirectToAction("Index", "Home");
	}

	[HttpGet]
	[Authorize]
	public IActionResult CompleteRegistration()
	{
		var role = this.User.FindFirstValue(ClaimTypes.Role);
		if (role == RoleEnum.Admin.ToString())
		{
			_logger.LogInformation("In admin complete registration");
			return this.RedirectToAction("AdminCompleteRegistration");
		}

		return this.View("CompleteRegistration");
	}

	/// <summary>
	/// AdminCompleteRegistration.
	/// </summary>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpGet]
	[Authorize]
	public Task<IActionResult> AdminCompleteRegistration()
	{
		return Task.FromResult<IActionResult>(this.View("AdminCompleteRegistration"));
	}

	/// <summary>
	/// AdminCompleteRegistration.
	/// </summary>
	/// <param name="model"> model. </param>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> AdminCompleteRegistration([FromForm] AdminCompleteRegistrationModel model)
	{
		this._logger.LogInformation("In complete registration");
		if (this.ModelState.IsValid)
		{
			try
			{
				await this._loginService.AdminCompleteRegistration(this.User.Identity!.Name ?? string.Empty, model.FullName, model.OrganizationName);
				this._logger.LogInformation("Admin complete registration");
				return this.RedirectToAction("Index", "Home");
			}
			catch (Exception error)
			{
				this._logger.LogError(error.Message);
			}
		}

		return this.View(model);
	}

	/// <summary>
	/// CompleteRegistration.
	/// </summary>
	/// <param name="model"> model. </param>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CompleteRegistration([FromForm] CompleteRegistrationModel model)
	{
		if (this.ModelState.IsValid)
		{
			try
			{
				await this._loginService.CompleteRegistration(this.User.Identity!.Name ?? string.Empty, model.Password, model.ConfirmPassword);
				this._logger.LogInformation("User complete registration");
				return this.RedirectToAction("Index", "Home");
			}
			catch (Exception error)
			{
				this._logger.LogError(error.Message);
			}
		}

		return this.View(model);
	}

	/// <summary>
	/// Forgot password.
	/// </summary>
	/// <param name="email"> email. </param>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpPost]
	public Task<IActionResult> ForgotPassword([FromForm] string email)
	{
		// connect with email
		return Task.FromResult<IActionResult>(this.View("Index"));
	}

	/// <summary>
	/// Forgot password.
	/// </summary>
	/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
	[HttpGet]
	public Task<IActionResult> ForgotPassword()
	{
		return Task.FromResult<IActionResult>(this.View("ForgotPassword"));
	}
}
}
