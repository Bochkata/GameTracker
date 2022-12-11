using GameTracker.Contracts;
using GameTracker.Data;
using GameTracker.Data.Entities;
using GameTracker.Models.Games.BoardGameModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static GameTracker.Data.DataConstants.BoardGameConstants;
using static GameTracker.Data.DataConstants.UserConstants;

namespace GameTracker.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly GameTrackerDbContext _gameTrackerDbContext;
        public BoardGameService(GameTrackerDbContext gameTrackerDbContext)
        {
            _gameTrackerDbContext = gameTrackerDbContext;
        }

        public async Task AddBoardGameAsync(BoardGameFormModel boardGameFormModel)
        {
            BoardGame boardGame = new BoardGame()
            {
                Name = boardGameFormModel.Name,
                Creator = boardGameFormModel.Creator,
                ImageUrl = boardGameFormModel.ImageUrl,
                BoardGameTypeId = boardGameFormModel.BoardGameTypeId,
                Rating = boardGameFormModel.Rating
            };
            await _gameTrackerDbContext.boardGames.AddAsync(boardGame);
            await _gameTrackerDbContext.SaveChangesAsync();
        }

        public async Task AddBoardGameToCollection(int boardGameId, string userId)
        {
        
            BoardGame boardGame = await _gameTrackerDbContext.boardGames.FirstOrDefaultAsync(x => x.Id == boardGameId);

            if (boardGame == null)
            {
                throw new ArgumentException(InvalidBoardGameId);
            }
            User user = await _gameTrackerDbContext.Users
                .Include(u => u.UsersFavoriteBoardGames)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);

            }
            if (!user.UsersFavoriteBoardGames.Any(BG => BG.BoardGameId == boardGameId))
            {
                user.UsersFavoriteBoardGames.Add(new UserFavoriteBoardGames { UserId = user.Id, BoardGameId = boardGameId });
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }

        public async Task<BoardGamesAllViewModel> FavoriteBoardGames(string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteBoardGames)
               .ThenInclude(ug => ug.BoardGame)
               .ThenInclude(ug => ug.BoardGameType)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }
            return new BoardGamesAllViewModel()
            {
                BoardGames = user.UsersFavoriteBoardGames.Select(BG => new BoardGameViewModel()
                {
                    Id = BG.BoardGame.Id,
                    Name = BG.BoardGame.Name,
                    Creator = BG.BoardGame.Creator,
                    BoardGameType = BG.BoardGame.BoardGameType?.Name,
                    ImageUrl = BG.BoardGame.ImageUrl,
                    Rating = BG.BoardGame.Rating,
                })
            };
        }

        public async Task<BoardGamesAllViewModel> GetAllBoardGamesAsync()
        {
            List<BoardGame> boardGames = await _gameTrackerDbContext.boardGames.Include(BG => BG.BoardGameType).ToListAsync();
            return new BoardGamesAllViewModel()
            {
                BoardGames = boardGames.Select(BG => new BoardGameViewModel()
                {
                    Id = BG.Id,
                    Name = BG.Name,
                    Creator = BG.Creator,
                    BoardGameType = BG.BoardGameType?.Name,
                    ImageUrl = BG.ImageUrl,
                    Rating = BG.Rating,
                })
            };
        }

        public  async Task<IEnumerable<BoardGameType>> GetAllBoardGameTypesAsync()
        {  
            return await _gameTrackerDbContext.boardGameTypes.ToListAsync();
        }

        public async Task RemoveBoardGameFromCollectionAsync(int boardGameId, string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteBoardGames)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }

            UserFavoriteBoardGames userGame = user.UsersFavoriteBoardGames.FirstOrDefault(ug => ug.BoardGameId == boardGameId);

            if (userGame != null)
            {
                user.UsersFavoriteBoardGames.Remove(userGame);
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }
        public  async Task Delete(int boardGameId)
        {
            BoardGame boardGame = _gameTrackerDbContext.boardGames.SingleOrDefault(g => g.Id == boardGameId);
            _gameTrackerDbContext.Entry(boardGame).State = EntityState.Deleted;

            _gameTrackerDbContext.SaveChanges();
        }
    }
}
