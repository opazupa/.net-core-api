using System.Threading.Tasks;
using API.GraphQL.Extensions;
using API.GraphQL.Subscriptions;
using API.GraphQL.Types;
using API.GraphQL.Types.Events;
using API.GraphQL.Types.Inputs;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using HotChocolate.Resolvers;
using HotChocolate.Types;

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
                    await TriggerCodingSkillEvents(ctx, skill);
                    return skill;
                });
            descriptor
                .Field("updateSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<IdType>>().Description("Coding skill id"))
                .Argument(SKILL, arg => arg.Type<NonNullType<CodingSkillInputType>>().Description("Coding skill info"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var skill = await ctx.Service<ICodingSkillService>().Update(ctx.Argument<long>(ID), ctx.Argument<CodingSkillEntity>(SKILL));
                    await ctx.Save();

                    // Send events
                    await TriggerCodingSkillEvents(ctx, skill);
                    return skill;
                });
            descriptor
                .Field("deleteSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<IdType>>().Description("Coding skill id"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var id = ctx.Argument<long>(ID);

                    var skill = await ctx.Service<ICodingSkillService>().Delete(id);
                    await ctx.Save();

                    // Send events
                    await TriggerCodingSkillEvents(ctx, skill);
                    return skill;
                });
        }

        /// <summary>
        /// Trigegr coding skill events
        /// </summary>
        private async Task TriggerCodingSkillEvents(IResolverContext ctx, CodingSkillEntity skill)
        {
            await Task.WhenAll(new []{
                ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_BY_LEVEL, skill.Level, skill)),
                ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_BY_NAME, skill.Name, skill)),
                ctx.TriggerEvent(new CodingSkillEvent(Events.ON_SKILL_BY_USER, skill.UserId, skill))
            });
        }
    }
}
