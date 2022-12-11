using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameTracker.Data.Entities
{
    public class UserFavoriteComputerGames
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [ForeignKey(nameof(ComputerGame))]
        [Required]
        public int ComputerGameId { get; set; }

        public ComputerGame ComputerGame { get; set; } = null!;
    }
}
