using FeatureLibrary.Models.Entities;
using GraphQL.Types;

namespace API.GraphQL
{
    public class UserType : ObjectGraphType<UserEntity>
    {
        public UserType()
        {
            Name = "User";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("User Id");
            Field(x => x.UserName).Description("Username");
        }
    }
}
