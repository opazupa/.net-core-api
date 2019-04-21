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
            modelBuilder.Entity<CodingSkill>().ToTable("CodingSkill");

            modelBuilder.Entity<CodingSkill>().HasData(SeedData.CodingSkills);

        }
    }
}
