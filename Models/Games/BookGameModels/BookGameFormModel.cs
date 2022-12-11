using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BookGameConstants;
using GameTracker.Data.Entities;

namespace GameTracker.Models.Games.BookGameModels
{
    public class BookGameFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(WriterNameMaxLength, MinimumLength = WriterNameMinLength)]
        public string Writer { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = RatingDecimal)]
        [Range(typeof(decimal), RatingMin, RatingMax, ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        public int BookGameTypeId { get; set; }


        public IEnumerable<BookGameType> BookGameTypes = new List<BookGameType>();
    }
}
