using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Skill level for coding.
    /// </summary>
    public enum CodingSkillLevel
    {
        Beginner = 1,
        Ok = 2,
        GettingThingsDone = 3,
        Master = 4
    }

    /// <summary>
    /// Coding skill.
    /// </summary>
    public class CodingSkill
    {
        /// <summary>
        /// Skill identifier.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Skill name.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Skill level.
        /// </summary>
        [Required]
        public CodingSkillLevel Level { get; set; }
    }
}
