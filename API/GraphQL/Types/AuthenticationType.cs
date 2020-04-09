using FeatureLibrary.Models;
using GraphQL.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// Authentication type
    /// </summary>
    public class AuthenticationType : ObjectGraphType<AuthenticationResult>
    {
        public AuthenticationType()
        {
            Name = "Auth";
            Field(x => x.UserId, type: typeof(NonNullGraphType<IdGraphType>)).Description("User Id");
            Field(x => x.Token).Description("User JWT token");
        }
    }
}
