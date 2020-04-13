using API.GraphQL.Types.Enums;
using FeatureLibrary.Models;
using HotChocolate.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// Auhtentication type
    /// </summary>
    public class AuhtenticationType : ObjectType<AuthenticationResult>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthenticationResult> descriptor)
        {
            Name = "Authentication";

            descriptor
                .Field(x => x.UserName)
                .Description("Username");

            descriptor
                .Field(x => x.Token)
                .Description("Auth token");

            descriptor
                .Field(x => x.TokenType)
                .Type<NonNullType<AuthTokenType>>()
                .Description("Auth token type");

            descriptor
                .Field(x => x.ExpiresIn)
                .Description("Token expiration date");
        }
    }
}
