using System.Collections.Generic;
using System.Linq;
using FeatureLibrary.Models;
using static FeatureLibrary.Database.MockData;

namespace FeatureLibrary.Database
{
    public static class SeedData
    {
        /// <summary>
        /// Admin user for testing
        /// </summary>
        public static readonly User ADMIN_USER = new User
        {
            Id = 1,
            Name = "admin",
            Password = "admin"
        };

        public static readonly List<User> Users = GetUsers(2).Concat(new[] { ADMIN_USER }).ToList();
        public static readonly List<CodingSkill> CodingSkills = GetSkills(15, Users).ToList();
    }
}
