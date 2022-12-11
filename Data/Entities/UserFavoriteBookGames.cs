using GameTracker.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameTracker.Data.Entities
{
    public class UserFavoriteBookGames
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [ForeignKey(nameof(BookGame))]
        [Required]
        public int BookGameId { get; set; }

        public BookGame BookGame { get; set; } = null!;
    }
}
