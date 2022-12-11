namespace GameTracker.Models.Games.BoardGameModels
{
    public class BoardGamesAllViewModel
    {
        public IEnumerable<BoardGameViewModel> BoardGames { get; set; } = new List<BoardGameViewModel>();
    }
}
