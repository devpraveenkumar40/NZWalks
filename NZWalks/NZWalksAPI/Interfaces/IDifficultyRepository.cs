using NZWalksAPI.Helpers;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync(QueryObject query);
        Task<Difficulty?> GetByIdAsync(Guid id);
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(Guid id);
    }
}
