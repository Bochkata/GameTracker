namespace GameTracker.Models.Games.BoardGameModels
{
    public class BoardGameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }
        public string? BoardGameType { get; set; }
    }
}
