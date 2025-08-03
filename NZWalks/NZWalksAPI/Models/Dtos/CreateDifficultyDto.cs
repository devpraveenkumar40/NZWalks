using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dtos
{
    public class CreateDifficultyDto
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; } = string.Empty;
    }
}
