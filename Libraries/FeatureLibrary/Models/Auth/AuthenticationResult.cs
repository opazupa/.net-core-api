namespace FeatureLibrary.Models
{
    /// <summary>
    /// Authentication result model
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// User Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
