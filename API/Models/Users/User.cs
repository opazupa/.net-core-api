using System.Collections.Generic;

namespace API.Models
{
    /// <summary>
    /// User API model
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User skills
        /// </summary>
        public IEnumerable<CodingSkill> Skills { get; set; } = new List<CodingSkill>();
    }
}
