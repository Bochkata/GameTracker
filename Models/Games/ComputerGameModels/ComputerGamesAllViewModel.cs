namespace GameTracker.Models.Games.ComputerGameModels
{
    public class ComputerGamesAllViewModel
    {
        public IEnumerable<ComputerGameViewModel> ComputerGames { get; set; } = new List<ComputerGameViewModel>();
    }
}
