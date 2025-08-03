using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Dtos.Image;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadDto)
        {
            ValidateFileUpload(imageUploadDto);
            if (ModelState.IsValid)
            {
                // Convert dto to domain model 
                var imageDomainModel = new Image
                {
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                    FileSizeInByte = imageUploadDto.File.Length,
                    FileName = imageUploadDto.FileName,
                    FileDescription = imageUploadDto.FileDescription
                };

                var image = await _imageRepository.Upload(imageDomainModel);

                if (image != null)
                {
                    var imageUploadResponseDto = new ImageUploadResponseDto
                    {
                        Id = image.Id,
                        FileName = image.FileName,
                        FileDescription = image.FileDescription,
                        FileExtension = image.FileExtension,
                        FileSizeInByte = image.FileSizeInByte,
                        FilePath = image.FilePath,
                        File = imageUploadDto.File
                    };
                    return Ok(imageUploadResponseDto);
                }
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ",png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than supported size");
            }
        }
    }
}
