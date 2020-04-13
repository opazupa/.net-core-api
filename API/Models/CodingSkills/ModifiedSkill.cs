using System.ComponentModel.DataAnnotations;
using FeatureLibrary.Models.Entities;

namespace API.Models
{
    /// <summary>
    /// Skill to modified in API
    /// </summary>
    public class ModifiedSkill
    {
        /// <summary>
        /// Skill name.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Skill level.
        /// </summary>
        [Required]
        public CodingSkillLevel? Level { get; set; }
    }
}
