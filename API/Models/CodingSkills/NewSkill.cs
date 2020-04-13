using System.ComponentModel.DataAnnotations;
using FeatureLibrary.Models.Entities;

namespace API.Models
{
    /// <summary>
    /// New skill to add in API
    /// </summary>
    public class NewSkill
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
