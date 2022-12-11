using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.ComputerGameConstants;
using GameTracker.Data.Entities;

namespace GameTracker.Models.Games.ComputerGameModels
{
    public class ComputerGameFormModel
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


        public int ComputerGameTypeId { get; set; }


        public IEnumerable<ComputerGameType> ComputerGameTypes = new List<ComputerGameType>();
    }
}
