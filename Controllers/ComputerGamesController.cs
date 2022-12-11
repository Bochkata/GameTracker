using GameTracker.Contracts;
using GameTracker.Models.Games.ComputerGameModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GameTracker.Data.DataConstants.ControllerConstants;
using static GameTracker.Data.DataConstants.ComputerGameConstants;
using GameTracker.Services;
using Microsoft.AspNetCore.Authorization;
using static GameTracker.Data.DataConstants;

namespace GameTracker.Controllers
{
    [Authorize]
    public class ComputerGamesController : Controller
    {
        private readonly IComputerGameService _computerGameService;
        public ComputerGamesController(IComputerGameService computerGameService)
        {
            _computerGameService = computerGameService;
        }

        [HttpGet]
        public async Task<IActionResult> AllComputerGames()
        {
            ComputerGamesAllViewModel ComputerGameAllViewModel = await _computerGameService.GetAllComputerGamesAsync();
            return View(ComputerGameAllViewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddComputerGame()
        {
            ComputerGameFormModel computerGameFormModel = new ComputerGameFormModel()
            {
                ComputerGameTypes = await _computerGameService.GetAllComputerGameTypesAsync()
            };
            return View(computerGameFormModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddComputerGame(ComputerGameFormModel computerGameFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(computerGameFormModel);
            }
            try
            {
                await _computerGameService.AddComputerGameAsync(computerGameFormModel);
                return RedirectToAction(nameof(AllComputerGames));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, InvalidComputerGameMessage);
                return View(computerGameFormModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int computerGameId)
        {
            try
            {

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _computerGameService.AddComputerGameToCollection(computerGameId, userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return RedirectToAction(nameof(AllComputerGames));
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteComputerGames()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ComputerGamesAllViewModel computerGamesAllViewModel = null!;
            try
            {
                computerGamesAllViewModel = await _computerGameService.FavoriteComputerGames(userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return View("FavoriteComputerGames", computerGamesAllViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int computerGameId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _computerGameService.RemoveComputerGameFromCollectionAsync(computerGameId, userId);
            return RedirectToAction(nameof(FavoriteComputerGames));
        }
        [HttpPost]
        public ActionResult Delete(int computerGameId)
        {
            _computerGameService.Delete(computerGameId);
            return RedirectToAction(nameof(AllComputerGames));
        }

    }
}
