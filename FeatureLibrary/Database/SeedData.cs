using System;
using System.Collections.Generic;
using System.Linq;
using FeatureLibrary.Models;
using static FeatureLibrary.Database.SkillMock;

namespace FeatureLibrary.Database
{
    public static class SeedData
    {
        public static readonly List<CodingSkill> CodingSkills = GetSkills(15).ToList();
    }
}
