using System.Collections.Generic;
using System.Linq;
using FeatureLibrary.Models;
using static FeatureLibrary.Database.MockData;

namespace FeatureLibrary.Database
{
    public static class SeedData
    {
        public static readonly List<CodingSkill> CodingSkills = GetSkills(15).ToList();
    }
}
