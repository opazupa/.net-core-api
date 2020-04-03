using Microsoft.EntityFrameworkCore;
using FeatureLibrary.Models;

namespace FeatureLibrary.Database
{
    /// <summary>
    /// Feature context.
    /// </summary>
    public class FeatureContext : DbContext
    {
        public FeatureContext(DbContextOptions<FeatureContext> options) : base(options)
        {
        }

        public DbSet<CodingSkill> CodingSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            modelBuilder.Entity<CodingSkill>(entity => {
                entity.ToTable(nameof(CodingSkill));
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name).IsUnique();

                // Seed data :)
                entity.HasData(SeedData.CodingSkills);
            });
        }
    }
}
