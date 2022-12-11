using GameTracker.Data.Entities;
using GameTracker.Data;
using GameTracker.Models.Games.ComputerGameModels;
using static GameTracker.Data.DataConstants.ComputerGameConstants;
using static GameTracker.Data.DataConstants.UserConstants;
using Microsoft.EntityFrameworkCore;
using GameTracker.Contracts;

namespace GameTracker.Services
{
    public class ComputerGameService :IComputerGameService
    {
        private readonly GameTrackerDbContext _gameTrackerDbContext;
        public ComputerGameService(GameTrackerDbContext gameTrackerDbContext)
        {
            _gameTrackerDbContext = gameTrackerDbContext;
        }

        public async Task AddComputerGameAsync(ComputerGameFormModel computerGameFormModel)
        {
            ComputerGame computerGame = new ComputerGame()
            {
                Name = computerGameFormModel.Name,
                Creator = computerGameFormModel.Creator,
                ImageUrl = computerGameFormModel.ImageUrl,
                ComputerGameTypeId = computerGameFormModel.ComputerGameTypeId,
                Rating = computerGameFormModel.Rating
            };
            await _gameTrackerDbContext.computerGames.AddAsync(computerGame);
            await _gameTrackerDbContext.SaveChangesAsync();
        }

        public async Task AddComputerGameToCollection(int computerGameId, string userId)
        {

            ComputerGame computerGame = await _gameTrackerDbContext.computerGames.FirstOrDefaultAsync(x => x.Id == computerGameId);

            if (computerGame == null)
            {
                throw new ArgumentException(InvalidComputerGameId);
            }
            User user = await _gameTrackerDbContext.Users
                .Include(u => u.UsersFavoriteComputerGames)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);

            }
            if (!user.UsersFavoriteComputerGames.Any(CG => CG.ComputerGameId == computerGameId))
            {
                user.UsersFavoriteComputerGames.Add(new UserFavoriteComputerGames { UserId = user.Id, ComputerGameId = computerGameId });
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }

        public async Task<ComputerGamesAllViewModel> FavoriteComputerGames(string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteComputerGames)
               .ThenInclude(ug => ug.ComputerGame)
               .ThenInclude(ug => ug.ComputerGameType)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }
            return new ComputerGamesAllViewModel()
            {
                ComputerGames = user.UsersFavoriteComputerGames.Select(CG => new ComputerGameViewModel()
                {
                    Id = CG.ComputerGame.Id,
                    Name = CG.ComputerGame.Name,
                    Creator = CG.ComputerGame.Creator,
                    ComputerGameType = CG.ComputerGame.ComputerGameType?.Name,
                    ImageUrl = CG.ComputerGame.ImageUrl,
                    Rating = CG.ComputerGame.Rating,
                })
            };
        }

        public async Task<ComputerGamesAllViewModel> GetAllComputerGamesAsync()
        {
            List<ComputerGame> computerGames = await _gameTrackerDbContext.computerGames.Include(CG => CG.ComputerGameType).ToListAsync();
            return new ComputerGamesAllViewModel()
            {
                ComputerGames = computerGames.Select(CG => new ComputerGameViewModel()
                {
                    Id = CG.Id,
                    Name = CG.Name,
                    Creator = CG.Creator,
                    ComputerGameType = CG.ComputerGameType?.Name,
                    ImageUrl = CG.ImageUrl,
                    Rating = CG.Rating,
                })
            };
        }
        public async Task<IEnumerable<ComputerGameType>> GetAllComputerGameTypesAsync()
        {
            return await _gameTrackerDbContext.computerGameTypes.ToListAsync();
        }

        public async Task RemoveComputerGameFromCollectionAsync(int computerGameId, string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteComputerGames)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }

            UserFavoriteComputerGames userGame = user.UsersFavoriteComputerGames.FirstOrDefault(ug => ug.ComputerGameId == computerGameId);

            if (userGame != null)
            {
                user.UsersFavoriteComputerGames.Remove(userGame);
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }
        public async Task Delete(int computerGameId)
        {
            ComputerGame computerGame = _gameTrackerDbContext.computerGames.SingleOrDefault(g => g.Id == computerGameId);
            _gameTrackerDbContext.Entry(computerGame).State = EntityState.Deleted;
            _gameTrackerDbContext.SaveChanges();
        }
    }
}
