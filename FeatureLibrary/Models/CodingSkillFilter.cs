using FeatureLibrary.Models;

namespace API.FilterModels
{
    /// <summary>
    /// Coding skill filter.
    /// </summary>
    public class CodingSkillFilter
    {
        public CodingSkillLevel Level { get; set; }
        public string Name { get; set; }
    }
}
