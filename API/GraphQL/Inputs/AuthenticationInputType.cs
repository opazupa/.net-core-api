using GraphQL.Types;

namespace API.GraphQL.Inputs
{
    /// <summary>
    /// Authentication input type
    /// </summary>
    public class AuthenticationInputType : InputObjectGraphType
    {
        public AuthenticationInputType()
        {
            Name = "AuthenticationInput";
            Field<NonNullGraphType<StringGraphType>>("UserName");
            Field<NonNullGraphType<StringGraphType>>("Password");
        }
    }
}
