using FeatureLibrary.Models.Entities;

namespace API.Models
{
    /// <summary>
    /// Coding skill API model
    /// </summary>
    public class CodingSkill
    {
        /// <summary>
        /// Skill id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Skill name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Skill level.
        /// </summary>
        public CodingSkillLevel Level { get; set; }
    }
}
