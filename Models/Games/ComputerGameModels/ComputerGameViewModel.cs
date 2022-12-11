namespace GameTracker.Models.Games.ComputerGameModels
{
    public class ComputerGameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }
        public string? ComputerGameType { get; set; }

    }
}
