using GameTracker.Data.Entities;
using GameTracker.Models.Games.BoardGameModels;

namespace GameTracker.Contracts
{
    public interface IBoardGameService
    {
        Task AddBoardGameAsync(BoardGameFormModel boardGameFormModel);

        Task<IEnumerable<BoardGameType>> GetAllBoardGameTypesAsync();

        Task<BoardGamesAllViewModel> GetAllBoardGamesAsync();

        Task AddBoardGameToCollection(int boardGameId, string userId);

        Task<BoardGamesAllViewModel> FavoriteBoardGames(string userId);

        Task RemoveBoardGameFromCollectionAsync(int boardGameId, string userId);

        Task Delete(int boardGameId);
    }
}
