using GameTracker.Data.Entities;
using GameTracker.Models.Games.ComputerGameModels;

namespace GameTracker.Contracts
{
    public interface IComputerGameService
    {

        Task AddComputerGameAsync(ComputerGameFormModel computerGameFormModel);

        Task<IEnumerable<ComputerGameType>> GetAllComputerGameTypesAsync();

        Task<ComputerGamesAllViewModel> GetAllComputerGamesAsync();

        Task AddComputerGameToCollection(int computerGameId, string userId);

        Task<ComputerGamesAllViewModel> FavoriteComputerGames(string userId);

        Task RemoveComputerGameFromCollectionAsync(int computerGameId, string userId);
        Task Delete(int computerGameId);
    }
}
