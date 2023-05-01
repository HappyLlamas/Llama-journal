// <copyright file="ItemManagementController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using LlamaJournal.Models;

namespace LlamaJournal.Controllers
{
    using BusinnesLayer.Services;

    /// <inheritdoc />
    [Authorize(Roles = "Teacher, Admin")]
    public class ItemManagementController : Controller
    {
        private readonly IDisciplineService _disciplineService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemManagementController"/> class.
        /// </summary>
        /// <param name="disciplineService"> discipline. </param>
        public ItemManagementController(IDisciplineService disciplineService)
        {
            this._disciplineService = disciplineService;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> Index()
        {
            // дисципліни, які веде викладач
            var userId = "myId";
            await this._disciplineService.GetAllDisciplines(userId);
            var infoItemCards = new List<InfoItemCard>
            {
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
                new InfoItemCard("phycics", "PMI-35"),
            };
            return this.View(infoItemCards);
        }

        // public async void AddGrade(int id)
        // {
        //     // добавлення оцінки у бд для конкрентого юзера
        // }
    }
}


// [HttpGet]
// public IActionResult DescribeMarks()
// {
//     return View();
// }
