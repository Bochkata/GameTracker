using GameTracker.Data.Entities;
using GameTracker.Models.Games.BookGameModels;

namespace GameTracker.Contracts
{
    public interface IBookGameService
    {
        Task AddBookGameAsync(BookGameFormModel bookGameFormModel);

        Task<IEnumerable<BookGameType>> GetAllBookGameTypesAsync();

        Task<BookGamesAllViewModel> GetAllBookGamesAsync();

        Task AddBookGameToCollection(int bookGameId, string userId);

        Task<BookGamesAllViewModel> FavoriteBookGames(string userId);

        Task RemoveBookGameFromCollectionAsync(int bookGameId, string userId);

        Task Delete(int bookGameId);
    }
}
