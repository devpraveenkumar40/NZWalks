using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Entities;

namespace NZWalksAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
                
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole> {
            new IdentityRole
            {
                Id = "38db9e59-aa95-491a-a840-5053f62f7d2b",
                ConcurrencyStamp = DateTime.Now.ToShortDateString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "bfbb9656-f3c8-4dec-849f-ed33a4cbc892",
                ConcurrencyStamp = DateTime.Now.ToShortDateString(),
                Name = "User",
                NormalizedName="USER"
            }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
