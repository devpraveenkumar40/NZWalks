using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
