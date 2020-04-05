using System.ComponentModel.DataAnnotations;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Authentication model
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}
