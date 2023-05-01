// <copyright file="HomeController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Controllers
{
    using BusinnesLayer.Services;
    using DataLayer.Models;

    /// <inheritdoc />
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="userService"> home controller. </param>
        public HomeController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await this._userService.GetUser(this.User.Identity!.Name ?? string.Empty);
            if (!user.CompleteRegistration)
            {
                return this.RedirectToAction("CompleteRegistration", "Login");
            }

            switch (user.Role)
            {
                case RoleEnum.Admin:
                    return this.RedirectToAction("Index", "Progress");
                case RoleEnum.Teacher:
                    return this.RedirectToAction("Index", "Progress");
                case RoleEnum.User:
                    return this.RedirectToAction("Index", "Progress");
            }

            return this.View();
        }
    }
}