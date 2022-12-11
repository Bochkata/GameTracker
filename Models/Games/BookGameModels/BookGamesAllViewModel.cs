namespace GameTracker.Models.Games.BookGameModels
{
    public class BookGamesAllViewModel
    {
        public IEnumerable<BookGameViewModel> BookGames { get; set; } = new List<BookGameViewModel>();
    }
}
