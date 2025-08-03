using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dtos
{
    public class DifficultyDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; } = string.Empty;
    }
}
