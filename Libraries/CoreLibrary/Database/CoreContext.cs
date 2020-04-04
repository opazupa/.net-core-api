using Microsoft.EntityFrameworkCore;
using CoreLibrary.Models;

namespace CoreLibrary.Database
{
    /// <summary>
    /// Core context.
    /// </summary>
    public class CoreContext<T> : DbContext where T : DbContext
    {
        public CoreContext(DbContextOptions<T> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            modelBuilder.Entity<User>(entity => {
                entity.ToTable(nameof(User));
                entity.HasIndex(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.Password);

                // Seed data :)
                entity.HasData(SeedData.Users);
            });
        }
    }
}
