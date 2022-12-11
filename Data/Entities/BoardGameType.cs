using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BoardGameTypeConstants;

namespace GameTracker.Data.Entities
{
    public class BoardGameType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BoardGameTypeNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
