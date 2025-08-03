using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Helpers;
using NZWalksAPI.Interfaces;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionModel = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionModel == null)
            {
                return null;
            }
            _dbContext.Regions.Remove(regionModel);
            await _dbContext.SaveChangesAsync();
            return regionModel;
        }

        public async Task<List<Region>> GetAllAsync(QueryObject query)
        {
            var regions = _dbContext.Regions.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Code))
            {
                regions = regions.Where(r => r.Code.Contains(query.Code));
            }

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                regions = regions.Where(r => r.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Code",StringComparison.OrdinalIgnoreCase))
                {
                    regions = query.IsDecsending ? regions.OrderByDescending(r => r.Code) : regions.OrderBy(r => r.Code);
                }
                else if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    regions = query.IsDecsending ? regions.OrderByDescending(r => r.Name) : regions.OrderBy(r => r.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await regions.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var regionModel = await _dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if (regionModel == null)
            {
                return null;
            }
            return regionModel;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            
            existingRegion.Name=region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
