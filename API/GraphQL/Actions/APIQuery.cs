using FeatureLibrary.Services;
using GraphQL.Types;

namespace API.GraphQL
{
    public class APIQuery : ObjectGraphType
    {
        #region Query consts
        private const string ID = "id";
        #endregion

        public APIQuery(IUserService userService)
        {
            FieldAsync<UserType>(
                Name = "User",
                arguments: new QueryArguments {
                    new QueryArgument<IdGraphType> { Name = ID, Description = "User Id"}
                },
                resolve: async context => {
                    return await userService.GetById(context.GetArgument<long>(ID));
                });
        }
    }
}
