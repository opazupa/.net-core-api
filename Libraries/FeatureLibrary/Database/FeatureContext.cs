using Microsoft.EntityFrameworkCore;
using FeatureLibrary.Models.Entities;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Feature context.
    /// </summary>
    public class FeatureContext : DbContext
    {
        public FeatureContext(DbContextOptions<FeatureContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CodingSkillEntity> CodingSkills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodingSkillEntity>(entity => {
                entity.ToTable(nameof(CodingSkillEntity));

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Level).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Name, e.UserId }).IsUnique();

                // Seed data :)
                entity.HasData(SeedData.CodingSkills);
            });

            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable(nameof(UserEntity));
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.UserName).IsUnique();
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
