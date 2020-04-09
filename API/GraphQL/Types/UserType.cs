using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using GraphQL.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// User type
    /// </summary>
    public class UserType : ObjectGraphType<UserEntity>
    {
        public UserType(ICodingSkillService codingSkillService)
        {
            Name = "User";
            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("User Id");
            Field(x => x.UserName).Description("Username");
            Field(x => x.Skills, type: typeof(ListGraphType<CodingSkillType>), nullable: false)
                .Description("User coding skills")
                .ResolveAsync(context => codingSkillService.GetByUserId(context.Source.Id));
        }
    }
}
