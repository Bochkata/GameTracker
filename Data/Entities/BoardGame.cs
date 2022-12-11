using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BoardGameConstants;
using GameTracker.Data.Entities;

namespace GameTracker.Data.Entities
{
    public class BoardGame
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CreatorNameMaxLength)]
        public string Creator { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }

        [ForeignKey(nameof(BoardGameType))]
        public int? BoardGameTypeId { get; set; }

        public BoardGameType? BoardGameType { get; set; }

        //[ForeignKey(nameof(User))]
        //[Required]
        //public string UserId { get; set; } = null!;
        //public User User { get; set; } = null!;

        public List<UserFavoriteBoardGames> UserGames = new List<UserFavoriteBoardGames>();
    }
}
