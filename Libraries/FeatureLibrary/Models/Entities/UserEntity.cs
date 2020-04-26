using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeatureLibrary.Models.Entities
{
    /// <summary>
    /// User entity model
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// User id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User skills
        /// </summary>
        public IEnumerable<CodingSkillEntity> Skills { get; set; } = new List<CodingSkillEntity>();
    }
}