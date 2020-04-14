using System.Linq;
using FeatureLibrary.Models.Entities;
using static FeatureLibrary.Models.MockData;

namespace FeatureLibrary.Models
{
    public static class SeedData
    {
        /// <summary>
        /// Admin user for testing
        /// </summary>
        public static readonly UserEntity ADMIN_USER = new UserEntity
        {
            Id = 9999999929939949,
            UserName = "admin",
            Password = "admin"
        };

        /// <summary>
        /// Ensure seed data to the context
        /// </summary>
        public static void EnsureSeedData(this FeatureContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    GetUsers(2)
                    .Concat(new[] { ADMIN_USER })
                );
                context.SaveChanges();
            }
            if (!context.CodingSkills.Any())
            {
                context.CodingSkills.AddRange(GetSkills(15, context.Users));
                context.SaveChanges();
            }
        }
    }
}
