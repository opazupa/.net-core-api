using API.GraphQL.Inputs;
using API.GraphQL.Types;
using FeatureLibrary.Models;
using FeatureLibrary.Services;
using GraphQL.Types;

namespace API.GraphQL.Mutations
{
    /// <summary>
    /// API mutations
    /// </summary>
    public class APIMutation : ObjectGraphType
    {
        private const string AUTH = "auth";

        public APIMutation(IUserService userService)
        {
            FieldAsync<AuthenticationType>(
                 "login",
                 arguments: new QueryArguments
                 {
                    new QueryArgument<NonNullGraphType<AuthenticationInputType>> { Name = AUTH}
                 },
                 resolve: async context =>
                 {
                     var auth = context.GetArgument<Authentication>(AUTH);
                     return await userService.Authenticate(auth);
                 }
             );
            FieldAsync<UserType>(
                 "register",
                 arguments: new QueryArguments
                 {
                    new QueryArgument<NonNullGraphType<AuthenticationInputType>> { Name = AUTH}
                 },
                 resolve: async context =>
                 {
                     var auth = context.GetArgument<Authentication>(AUTH);
                     return await userService.CreateUser(auth);
                 }
             );
        }
    }
}
