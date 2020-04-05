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

        public DbSet<User> Users { get; set; }
        public DbSet<CodingSkill> CodingSkills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodingSkill>(entity => {
                entity.ToTable(nameof(CodingSkill));

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Level).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Name, e.UserId }).IsUnique();

                // Seed data :)
                entity.HasData(SeedData.CodingSkills);
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable(nameof(User));
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Password).IsRequired();

                entity.HasMany(b => b.Skills)
                    .WithOne()
                    .HasForeignKey(b => b.UserId)
                    .IsRequired();

                // Seed data :)
                entity.HasData(SeedData.Users);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
