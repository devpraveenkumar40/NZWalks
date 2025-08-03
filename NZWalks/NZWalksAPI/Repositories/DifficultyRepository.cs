using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DifficultyRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await _dbContext.Difficulties.AddAsync(difficulty);
            await _dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var difficultyModel = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyModel == null)
            {
                return null;
            }
            _dbContext.Difficulties.Remove(difficultyModel);
            await _dbContext.SaveChangesAsync();
            return difficultyModel;
        }

        public async Task<List<Difficulty>> GetAllAsync(QueryObject query)
        {
            var difficulties = _dbContext.Difficulties.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                difficulties = difficulties.Where(r => r.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    difficulties = query.IsDecsending ? difficulties.OrderByDescending(r => r.Name) : difficulties.OrderBy(r => r.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await difficulties.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            var difficultyModel = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyModel == null)
            {
                return null;
            }
            return difficultyModel;
        }

        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var existingDifficulty = await _dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDifficulty == null)
            {
                return null;
            }

            existingDifficulty.Name = difficulty.Name;

            await _dbContext.SaveChangesAsync();
            return existingDifficulty;
        }
    }
}
