using System.Linq;
using FeatureLibrary.Models.Entities;
using static FeatureLibrary.Models.MockData;
using static BCrypt.Net.BCrypt;

namespace FeatureLibrary.Models
{
    public static class SeedData
    {
        public const string ADMIN = "admin";
        /// <summary>
        /// Admin user for testing
        /// </summary>
        private static readonly UserEntity ADMIN_USER = new UserEntity
        {
            Id = 9999999929939949,
            UserName = ADMIN,
            Password = ADMIN
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
                    .Select(u => {
                        // Hash passwords to the database
                        u.Password = HashPassword(u.Password);
                        return u;
                    })
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
