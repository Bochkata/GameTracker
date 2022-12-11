using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.BookGameTypeConstants;

namespace GameTracker.Data.Entities
{
    public class BookGameType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BookGameTypeNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
