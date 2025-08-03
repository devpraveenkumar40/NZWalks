using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly ApplicationDbContext dbContext;

        public WalksRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkModel == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walkModel);
            await dbContext.SaveChangesAsync();
            return walkModel;
        }

        public async Task<List<Walk>> GetAllAsync(WalkQueryObject query)
        {
            var walks = dbContext.Walks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                walks = walks.Where(w => w.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.LengthInKm.ToString()) && query.LengthInKm > 0)
            {
                walks = walks.Where(w => w.LengthInKm == query.LengthInKm);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = query.IsDecsending ? walks.OrderByDescending(w => w.Name) : walks.OrderBy(w => w.Name);
                }
                else if (query.SortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = query.IsDecsending ? walks.OrderByDescending(r => r.LengthInKm) : walks.OrderBy(r => r.LengthInKm);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await walks.Include("Difficulty").Include("Region").Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walkModel = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (walkModel == null)
            {
                return null;
            }
            return walkModel;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;            

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
