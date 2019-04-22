using System;
using System.Collections.Generic;
using FeatureLibrary.Models;

namespace FeatureLibrary.Database
{
    public static class SeedData
    {
        public static readonly List<CodingSkill> CodingSkills = new List<CodingSkill>(){
            new CodingSkill() { Id = 999, Name = "Python", Level = CodingSkillLevel.Ok },
            new CodingSkill() { Id = 99, Name = "C#", Level = CodingSkillLevel.Master }
        };
    }
}
