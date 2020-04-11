using API.GraphQL.Types.Inputs;
using API.GraphQL.Types;
using API.GraphQL.Extensions;
using FeatureLibrary.Extensions;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using HotChocolate.Types;
using Microsoft.AspNetCore.Http;

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
                .Field("Login")
                .Argument(AUTH, arg => arg.Type<NonNullType<AuthenticationInputType>>().Description("Authentication"))
                .Type<AuhtenticationType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<IUserService>().Authenticate(ctx.Argument<Authentication>(AUTH));
                });
            descriptor
                .Field("Register")
                .Argument(AUTH, arg => arg.Type<NonNullType<AuthenticationInputType>>().Description("Authentication"))
                .Type<UserType>()
                .Resolver(async ctx =>
                {
                    var user = await ctx.Service<IUserService>().CreateUser(ctx.Argument<Authentication>(AUTH));
                    await ctx.Save();
                    return user;
                });

            /*
            * Coding skills
            */
            descriptor
                .Field("AddSkill")
                .Authorize()
                .Argument(SKILL, arg => arg.Type<NonNullType<CodingSkillInputType>>().Description("Coding skill info"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var userId = ((HttpContext)ctx.ContextData[nameof(HttpContext)]).User.GetId();

                    var skill = await ctx.Service<ICodingSkillService>().Add(ctx.Argument<CodingSkillEntity>(SKILL), userId);
                    await ctx.Save();
                    return skill;
                });
            descriptor
                .Field("UpdateSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("Coding skill id"))
                .Argument(SKILL, arg => arg.Type<NonNullType<CodingSkillInputType>>().Description("Coding skill info"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    var skill = await ctx.Service<ICodingSkillService>().Update(ctx.Argument<long>(ID), ctx.Argument<CodingSkillEntity>(SKILL));
                    await ctx.Save();
                    return SKILL;
                });
            descriptor
                .Field("DeleteSkill")
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
