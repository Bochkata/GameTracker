using GameTracker.Contracts;
using GameTracker.Models.Games.BookGameModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GameTracker.Data.DataConstants.BookGameConstants;
using static GameTracker.Data.DataConstants.ControllerConstants;
using Microsoft.Extensions.FileSystemGlobbing;
using GameTracker.Services;
using Microsoft.AspNetCore.Authorization;
using static GameTracker.Data.DataConstants;

namespace GameTracker.Controllers
{
    [Authorize]
    public class BookGamesController : Controller
    {
        private readonly IBookGameService _bookGameService;
        public BookGamesController(IBookGameService bookGameService)
        {
            _bookGameService = bookGameService;
        }

        [HttpGet]
        public async Task<IActionResult> AllBookGames()
        {
            BookGamesAllViewModel BookGameAllViewModel = await _bookGameService.GetAllBookGamesAsync();
            return View(BookGameAllViewModel);
        }
        [HttpGet]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddBookGame()
        {
            BookGameFormModel bookGameFormModel = new BookGameFormModel()
            {
                BookGameTypes = await _bookGameService.GetAllBookGameTypesAsync()
            };
            return View(bookGameFormModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddBookGame(BookGameFormModel bookGameFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookGameFormModel);
            }
            try
            {
                await _bookGameService.AddBookGameAsync(bookGameFormModel);
                return RedirectToAction(nameof(AllBookGames));//to do
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, InvalidBookGameMessage);
                return View(bookGameFormModel);
            }
        }
            [HttpPost]
         public async Task<IActionResult> AddToCollection(int bookGameId)
        {
            try
            {

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _bookGameService.AddBookGameToCollection(bookGameId, userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return RedirectToAction(nameof(AllBookGames));
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteBookGames()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BookGamesAllViewModel bookGamesAllViewModel = null!;
            try
            {
                bookGamesAllViewModel = await _bookGameService.FavoriteBookGames(userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return View("FavoriteBookGames", bookGamesAllViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int bookGameId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _bookGameService.RemoveBookGameFromCollectionAsync(bookGameId, userId);
            return RedirectToAction(nameof(FavoriteBookGames));
        }
        [HttpPost]
        [Authorize(Roles = RoleContants.Admin)]
        public ActionResult Delete(int bookGameId)
        {
            _bookGameService.Delete(bookGameId);
            return RedirectToAction(nameof(AllBookGames));
        }


    }
}
