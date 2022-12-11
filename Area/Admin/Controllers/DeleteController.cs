using GameTracker.Contracts;
using GameTracker.Models.Games.BookGameModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GameTracker.Data.DataConstants.BookGameConstants;

namespace GameTracker.Area.Admin.Controllers
{
    public class DeleteController : BaseController
    {
        private readonly IBookGameService _bookGameService;
        public DeleteController(IBookGameService bookGameService)
        {
            _bookGameService = bookGameService;
        }
        [HttpPost]
        public ActionResult Delete(int bookGameId)
        {
            _bookGameService.Delete(bookGameId);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Index()
        {
            BookGamesAllViewModel BookGameAllViewModel = await _bookGameService.GetAllBookGamesAsync();
            return View(BookGameAllViewModel);
        }
    }
}
