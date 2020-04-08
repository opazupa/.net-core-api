using FeatureLibrary.Models.Entities;
using GraphQL.Types;

namespace API.GraphQL
{
    /// <summary>
    /// User type
    /// </summary>
    public class UserType : ObjectGraphType<UserEntity>
    {
        public UserType()
        {
            Name = "User";
            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("User Id");
            Field(x => x.UserName).Description("Username");
            Field(x => x.Skills, type: typeof(ListGraphType<CodingSkillType>), nullable: false).Description("User coding skills");
        }
    }
}
