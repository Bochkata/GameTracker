using GameTracker.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameTracker.Data.Entities
{
    public class UserFavoriteBoardGames
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [ForeignKey(nameof(BoardGame))]
        [Required]
        public int BoardGameId { get; set; } 

        public BoardGame BoardGame { get; set; } = null!;
    }
}
