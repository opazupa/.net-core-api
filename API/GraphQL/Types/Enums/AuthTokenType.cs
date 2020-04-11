using FeatureLibrary.Models;
using HotChocolate.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// Auth Token type
    /// </summary>
    public class AuthTokenType : EnumType<TokenType> { }
}
