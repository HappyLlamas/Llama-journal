// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace LlamaJournal.Controllers
{
    using BusinnesLayer.Services;

    /// <inheritdoc />
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService"> user. </param>
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> Index()
        {
            var users = await this._userService.GetUsers();
            return this.View(users);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <param name="id"> id. </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> Details(string id)
        {
            var user = await this._userService.GetUser(id);
            return this.View(user);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        // public IActionResult Edit(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string id, User user)
        // {
        //     if (id != user.Id)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (ModelState.IsValid)
        //     {
        //         _userRepository.SetGroup(user, user.Group.Name);
        //         _userRepository.SetRole(user, user.Role);
        //         _userRepository.SetUserPassword(user, user.Password);
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(user);
        // }
        //
        // public IActionResult Delete(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(user);
        // }
        //
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public IActionResult DeleteConfirmed(string id)
        // {
        //     var user = _userRepository.GetById(id);
        //     if (user == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _userRepository.DeleteUser(user);
        //     return RedirectToAction(nameof(Index));
        // }
    }
}
