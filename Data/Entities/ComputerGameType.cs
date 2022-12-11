using System.ComponentModel.DataAnnotations;
using static GameTracker.Data.DataConstants.ComputerGameTypeConstants;

namespace GameTracker.Data.Entities
{
    public class ComputerGameType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ComputerGameTypeNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
