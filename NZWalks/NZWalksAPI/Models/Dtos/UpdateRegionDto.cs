using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.Dtos
{
    public class UpdateRegionDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Code cannot be less than 5 charaters long")]
        [MaxLength(10, ErrorMessage = "Code cannot be more than 10 charaters long")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Name cannot be less than 5 charaters long")]
        [MaxLength(20, ErrorMessage = "Name cannot be more than 20 charaters long")]
        public string Name { get; set; } = string.Empty;

        public string? RegionImageUrl { get; set; }
    }
}
