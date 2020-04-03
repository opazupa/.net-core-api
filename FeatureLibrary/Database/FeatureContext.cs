using Microsoft.EntityFrameworkCore;
using FeatureLibrary.Models;
using static FeatureLibrary.Database.SkillMock;
using System.Linq;

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
                entity.ToTable("CodingSkill");
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name).IsUnique();

                // Seed data :)
                entity.HasData(SeedData.CodingSkills);
            });
        }
    }
}
