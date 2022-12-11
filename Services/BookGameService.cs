using GameTracker.Data.Entities;
using GameTracker.Data;
using GameTracker.Contracts;
using GameTracker.Models.Games.BookGameModels;
using Microsoft.EntityFrameworkCore;
using static GameTracker.Data.DataConstants.BookGameConstants;
using static GameTracker.Data.DataConstants.UserConstants;

namespace GameTracker.Services
{
    public class BookGameService: IBookGameService
    {
        private readonly GameTrackerDbContext _gameTrackerDbContext;
        public BookGameService(GameTrackerDbContext gameTrackerDbContext)
        {
            _gameTrackerDbContext = gameTrackerDbContext;
        }

        public async Task AddBookGameAsync(BookGameFormModel bookGameFormModel)
        {
            BookGame bookGame = new BookGame()
            {
                Title = bookGameFormModel.Title,
                Writer = bookGameFormModel.Writer,
                ImageUrl = bookGameFormModel.ImageUrl,
                BookGameTypeId = bookGameFormModel.BookGameTypeId,
                Rating = bookGameFormModel.Rating
            };
            await _gameTrackerDbContext.bookGames.AddAsync(bookGame);
            await _gameTrackerDbContext.SaveChangesAsync();
        }

        public async Task AddBookGameToCollection(int bookGameId, string userId)
        {

            BookGame bookGame = await _gameTrackerDbContext.bookGames.FirstOrDefaultAsync(x => x.Id == bookGameId);
            
            if (bookGame == null)
            {
                throw new ArgumentException(InvalidBookGameId);
            }
            User user = await _gameTrackerDbContext.Users
                .Include(u => u.UsersFavoriteBookGames)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);

            }
            if (!user.UsersFavoriteBookGames.Any(BG => BG.BookGameId == bookGameId))
            {
                user.UsersFavoriteBookGames.Add(new UserFavoriteBookGames { UserId = user.Id, BookGameId = bookGameId });
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }

        public async Task<BookGamesAllViewModel> FavoriteBookGames(string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteBookGames)
               .ThenInclude(ug => ug.BookGame)
               .ThenInclude(ug => ug.BookGameType)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }
            return new BookGamesAllViewModel()
            {
                BookGames = user.UsersFavoriteBookGames.Select(BG => new BookGameViewModel()
                {
                    Id = BG.BookGame.Id,
                    Title = BG.BookGame.Title,
                    Writer = BG.BookGame.Writer,
                    BookGameType = BG.BookGame.BookGameType?.Name,
                    ImageUrl = BG.BookGame.ImageUrl,
                    Rating = BG.BookGame.Rating,
                })
            };
        }

        public async Task<BookGamesAllViewModel> GetAllBookGamesAsync()
        {
            List<BookGame> bookGames = await _gameTrackerDbContext.bookGames.Include(BG => BG.BookGameType).ToListAsync();
            return new BookGamesAllViewModel()
            {
                BookGames = bookGames.Select(BG => new BookGameViewModel()
                {
                    Id = BG.Id,
                    Title = BG.Title,
                    Writer = BG.Writer,
                    BookGameType = BG.BookGameType?.Name,
                    ImageUrl = BG.ImageUrl,
                    Rating = BG.Rating,
                })
            };
        }
        public async Task<IEnumerable<BookGameType>> GetAllBookGameTypesAsync()
        {
            return await _gameTrackerDbContext.bookGameTypes.ToListAsync();
        }

        public async Task RemoveBookGameFromCollectionAsync(int bookGameId, string userId)
        {
            User user = await _gameTrackerDbContext.Users
               .Include(u => u.UsersFavoriteBookGames)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(InvalidUserId);
            }

            UserFavoriteBookGames userGame = user.UsersFavoriteBookGames.FirstOrDefault(ug => ug.BookGameId == bookGameId);

            if (userGame != null)
            {
                user.UsersFavoriteBookGames.Remove(userGame);
                await _gameTrackerDbContext.SaveChangesAsync();
            }
        }
        public async Task Delete(int bookGameId)
        {
            BookGame bookGame = _gameTrackerDbContext.bookGames.SingleOrDefault(g => g.Id == bookGameId);
            _gameTrackerDbContext.Entry(bookGame).State = EntityState.Deleted;
            _gameTrackerDbContext.SaveChanges();
        }
    }
}
