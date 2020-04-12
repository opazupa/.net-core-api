using API.GraphQL.Types.Events;
using API.GraphQL.Types.Inputs;
using API.GraphQL.Types;
using API.GraphQL.Extensions;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using HotChocolate.Types;
using API.GraphQL.Subscriptions;

namespace API.GraphQL.Mutations
{
    /// <summary>
    /// API mutations
    /// </summary>
    public class APIMutation : ObjectType
    {
        #region Mutation consts
        private const string AUTH = "auth";
        private const string ID = "id";
        private const string SKILL = "skill";
        #endregion
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            /*
            * Auth
            */
            descriptor
                .Field("login")
                .Argument(AUTH, arg => arg.Type<NonNullType<AuthenticationInputType>>().Description("Authentication"))
                .Type<AuhtenticationType>()
                .Resolver(async ctx =>
                {
                    var auth = await ctx.Service<IUserService>().Authenticate(ctx.Argument<Authentication>(AUTH));

                    // Send event
                    await ctx.TriggerEvent(new UserEvent(Events.ON_USER_LOGIN, auth.UserName));
                    return auth;
                });
            descriptor
                .Field("register")
                .Argument(AUTH, arg => arg.Type<NonNullType<AuthenticationInputType>>().Description("Authentication"))
                .Type<UserType>()
                .Resolver(async ctx =>
                {
                    var user = await ctx.Service<IUserService>().CreateUser(ctx.Argument<Authentication>(AUTH));
                    await ctx.Save();

                    // Send event
                    await ctx.TriggerEvent(new UserEvent(Events.ON_USER_REGISTER, user.UserName));
                    return user;
                });

            /*
            * Coding skills
            */
            descriptor
                .Field("addSkill")
                .Authorize()
                .Argument(SKILL, arg => arg.Type<NonNullType<CodingSkillInputType>>().Description("Coding skill info"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var userId = ctx.GetUserId();

                    var skill = await ctx.Service<ICodingSkillService>().Add(ctx.Argument<CodingSkillEntity>(SKILL), userId);
                    await ctx.Save();

                    // Send events
                    await ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_UPDATE_BY_LEVEL, skill.Level, skill));
                    await ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_UPDATE_BY_NAME, skill.Name, skill));
                    return skill;
                });
            descriptor
                .Field("updateSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("Coding skill id"))
                .Argument(SKILL, arg => arg.Type<NonNullType<CodingSkillInputType>>().Description("Coding skill info"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var skill = await ctx.Service<ICodingSkillService>().Update(ctx.Argument<long>(ID), ctx.Argument<CodingSkillEntity>(SKILL));
                    await ctx.Save();

                    // Send events
                    await ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_UPDATE_BY_LEVEL, skill.Level, skill));
                    await ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_UPDATE_BY_NAME, skill.Name, skill));
                    return SKILL;
                });
            descriptor
                .Field("deleteSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("Coding skill id"))
                .Type<LongType>()
                .Resolver(async ctx =>
                {
                    var id = ctx.Argument<long>(ID);

                    await ctx.Service<ICodingSkillService>().Delete(id);
                    await ctx.Save();
                    return ID;
                });
        }
    }
}
