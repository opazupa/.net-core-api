using System;

namespace FeatureLibrary.Models
{
    /// <summary>
    /// Authentication token type
    /// </summary>
    public enum TokenType
    {
        Bearer = 1
    }

    /// <summary>
    /// Authentication result model
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Tokentype
        /// </summary>
        public TokenType TokenType { get; set; } = TokenType.Bearer;
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public DateTime ExpiresIn { get; set; } 
    }
}
