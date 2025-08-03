using NZWalksAPI.Helpers;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Interfaces
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync(WalkQueryObject query);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
