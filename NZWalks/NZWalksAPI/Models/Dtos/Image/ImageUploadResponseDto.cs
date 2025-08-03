using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Dtos.Image
{
    public class ImageUploadResponseDto
    {
        public Guid Id { get; set; }        
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInByte { get; set; }
        public string FilePath { get; set; }
    }
}
