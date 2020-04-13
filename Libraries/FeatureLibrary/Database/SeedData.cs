using System.Collections.Generic;
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
            Id = 1,
            UserName = "admin",
            Password = "admin"
        };

        public static readonly List<UserEntity> Users = GetUsers(2).Concat(new[] { ADMIN_USER }).ToList();
        public static readonly List<CodingSkillEntity> CodingSkills = GetSkills(15, Users).ToList();
    }
}
