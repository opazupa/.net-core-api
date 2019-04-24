using System.Collections.Generic;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Coding skill filter.
    /// </summary>
    public class CodingSkillFilter
    {
        public List<CodingSkillLevel> Levels { get; set; }
        public string Name { get; set; }
    }
}
