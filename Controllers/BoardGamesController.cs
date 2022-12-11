using GameTracker.Contracts;
using GameTracker.Models.Games.BoardGameModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GameTracker.Data.DataConstants.ControllerConstants;
using static GameTracker.Data.DataConstants.BoardGameConstants;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using static GameTracker.Data.DataConstants;

namespace GameTracker.Controllers
{
    [Authorize]
    public class BoardGamesController : Controller
    {
        private readonly IBoardGameService _boardGameService;
        public BoardGamesController(IBoardGameService boardGameService)
        {
            _boardGameService = boardGameService;
        }

        [HttpGet]
        public async Task<IActionResult> AllBoardGames()
        {
            BoardGamesAllViewModel BoardGameAllViewModel = await _boardGameService.GetAllBoardGamesAsync();
            return View(BoardGameAllViewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddBoardGame()
        {
            BoardGameFormModel boardGameFormModel = new BoardGameFormModel()
            {
                BoardGameTypes = await _boardGameService.GetAllBoardGameTypesAsync()
            };
            return View(boardGameFormModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleContants.Admin)]
        public async Task<IActionResult> AddBoardGame(BoardGameFormModel boardGameFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(boardGameFormModel);
            }
            try
            {
                await _boardGameService.AddBoardGameAsync(boardGameFormModel);
                return RedirectToAction(nameof(AllBoardGames));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, InvalidBoardGameMessage);
                return View(boardGameFormModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int boardGameId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _boardGameService.AddBoardGameToCollection(boardGameId, userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return RedirectToAction(nameof(AllBoardGames));
        }
        [HttpGet]
        public async Task<IActionResult> FavoriteBoardGames()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BoardGamesAllViewModel boardGamesAllViewModel = null!;
            try
            {
                boardGamesAllViewModel = await _boardGameService.FavoriteBoardGames(userId);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralErrorMessage);
            }
            return View("FavoriteBoardGames", boardGamesAllViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int boardGameId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _boardGameService.RemoveBoardGameFromCollectionAsync(boardGameId, userId);
            return RedirectToAction(nameof(FavoriteBoardGames));
        }
        [HttpPost]
        public ActionResult Delete(int boardGameId)
        {
            _boardGameService.Delete(boardGameId);
            return RedirectToAction(nameof(AllBoardGames));
        }


    }
}
