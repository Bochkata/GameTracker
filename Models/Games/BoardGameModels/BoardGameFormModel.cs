using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BoardGameConstants;
using GameTracker.Data.Entities;

namespace GameTracker.Models.Games.BoardGameModels
{
    public class BoardGameFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(CreatorNameMaxLength, MinimumLength = CreatorNameMinLength)]
        public string Creator { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Column(TypeName = RatingDecimal)]
        [Range(typeof(decimal), RatingMin, RatingMax, ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        public int BoardGameTypeId { get; set; }

        public IEnumerable<BoardGameType> BoardGameTypes = new List<BoardGameType>();
    }
}
