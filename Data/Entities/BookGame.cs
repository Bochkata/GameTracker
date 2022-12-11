using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BookGameConstants;
using GameTracker.Data.Entities;

namespace GameTracker.Data.Entities
{
    public class BookGame
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(WriterNameMaxLength)]
        public string Writer { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; } 

        [ForeignKey(nameof(BookGameType))]
        public int? BookGameTypeId { get; set; }

        public BookGameType? BookGameType { get; set; }

        //[ForeignKey(nameof(User))]
        //[Required]
        //public string UserId { get; set; } = null!;
        //public User User { get; set; } = null!;

        public List<UserFavoriteBoardGames> UserGames = new List<UserFavoriteBoardGames>();

    }
}
