using Microsoft.EntityFrameworkCore;
using FeatureLibrary.Models.Entities;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Feature context.
    /// </summary>
    public class FeatureContext : DbContext
    {
        public FeatureContext() : base() {}
        public FeatureContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=example;Server=db;Port=5432;Database=API_DB;Integrated Security=true;Pooling=true;");
            }
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CodingSkillEntity> CodingSkills { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CodingSkillEntity>(entity => {
                entity.ToTable("CodingSkill");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Level).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Name, e.UserId }).IsUnique();
            });

            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.Password).IsRequired();

                entity.HasMany(b => b.Skills)
                    .WithOne()
                    .HasForeignKey(b => b.UserId)
                    .IsRequired();
            });
        }
    }
}
