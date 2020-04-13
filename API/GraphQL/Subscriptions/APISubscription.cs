using API.GraphQL.Types;
using API.GraphQL.Types.Enums;
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
            descriptor
                .BindFieldsExplicitly()
                .Authorize();
            /*
             * Users
             */
            descriptor
                .Name(Events.ON_USER_LOGIN)
                .Field(x => x.OnUserLogin(default))                
                .Type<NonNullType<StringType>>();
            descriptor
                .Name(Events.ON_USER_REGISTER)
                .Field(x => x.OnUserRegister(default))
                .Type<NonNullType<StringType>>();

            /*
            * Coding skills
            */
            descriptor
                .Name(Events.ON_SKILL_BY_LEVEL)
                .Field(x => x.OnSkillByLevel(default, default))
                .Type<NonNullType<CodingSkillType>>()
                .Argument("level", arg => arg.Type<NonNullType<CodingSkillLevelType>>());
            descriptor
                .Field(x => x.OnSkillByName(default, default))
                .Name(Events.ON_SKILL_BY_NAME)
                .Type<NonNullType<CodingSkillType>>()
                .Argument("name", arg => arg.Type<NonNullType<StringType>>());
            descriptor
                .Field(x => x.OnSkillByUser(default, default))
                .Name(Events.ON_SKILL_BY_USER)
                .Type<NonNullType<CodingSkillType>>()
                .Argument("userId", arg => arg.Type<NonNullType<LongType>>());
        }
    }
}