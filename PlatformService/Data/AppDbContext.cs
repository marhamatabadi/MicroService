using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(500).IsRequired();
                entity.Property(x => x.Publisher).HasMaxLength(500).IsRequired();
                entity.Property(x => x.Cost).HasMaxLength(500).IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Platform> Platforms { get; set; }
    }
}
