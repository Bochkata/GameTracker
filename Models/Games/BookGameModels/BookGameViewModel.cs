namespace GameTracker.Models.Games.BookGameModels
{
    public class BookGameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string Writer { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }
        public string? BookGameType { get; set; }
    }
}
