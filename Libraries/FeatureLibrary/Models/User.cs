using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
        /// <summary>
        /// User skills
        /// </summary>
        public IEnumerable<CodingSkill> Skills { get; set; } = new List<CodingSkill>();
    }
}