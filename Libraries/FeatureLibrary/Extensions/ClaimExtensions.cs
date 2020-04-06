using System.Linq;
using System.Security.Claims;

namespace FeatureLibrary.Extensions
{
    /// <summary>
    /// Extrnsions for claim handling
    /// </summary>
    public static class ClaimExtensions
    {
        internal const string USER_ID = "USER_ID";

        /// <summary>
        /// Get User Id
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static long GetId(this ClaimsPrincipal User)
        {
            var claim = User.Claims.Single(a => a.Type == USER_ID);
            return long.TryParse(claim.Value, out var val) ? val : 0;
        }
    }
}
