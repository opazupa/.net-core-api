using API.GraphQL.Types;
using API.GraphQL.Types.Enums;
using FeatureLibrary.Models.Entities;
using HotChocolate.Types;

namespace API.GraphQL.Subscriptions
{
    /// <summary>
    /// API Subscription type
    /// </summary>
    public class APISubscription : ObjectType<Events>
    {   
        protected override void Configure(IObjectTypeDescriptor<Events> descriptor)
        {
            /*
             * Users
             */
            descriptor
                .Name(Events.ON_USER_LOGIN)
                .Field(x => x.OnUserLogin(default))                
                .Authorize()
                .Type<NonNullType<StringType>>();
            descriptor
                .Name(Events.ON_USER_REGISTER)
                .Field(x => x.OnUserRegister(default))
                .Authorize()
                .Type<NonNullType<StringType>>();

            /*
            * Coding skills
            */
            descriptor
                .Name(Events.ON_SKILL_UPDATE_BY_LEVEL)
                .Field(x => x.OnSkillUpdateByLevel(default, default))
                .Authorize()
                .Type<NonNullType<CodingSkillType>>()
                .Argument("level", arg => arg.Type<NonNullType<CodingSkillLevelType>>());
            descriptor
                .Name(Events.ON_SKILL_UPDATE_BY_NAME)
                .Field(x => x.OnSkillUpdateByName(default, default))
                .Authorize()
                .Type<NonNullType<CodingSkillType>>()
                .Argument("name", arg => arg.Type<NonNullType<StringType>>());
        }
    }
}