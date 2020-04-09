using System.Security.Claims;
using API.GraphQL.Inputs;
using API.GraphQL.Types;
using CoreLibrary.Services.Persistence;
using FeatureLibrary.Extensions;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
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
        private const string ID = "id";
        private const string SKILL = "skill";

        public APIMutation(
            IPersistenceService dbTransaction, 
            IUserService userService, 
            ICodingSkillService codingSkillService
        )
        {
            // Auth
            FieldAsync<AuthenticationType>(
                "Login",
                arguments: new QueryArguments
                {
                new QueryArgument<NonNullGraphType<AuthenticationInputType>> { Name = AUTH}
                },
                resolve: async context =>
                {
                    var auth = context.GetArgument<Authentication>(AUTH);
                    return await userService.Authenticate(auth);
                });
            FieldAsync<UserType>(
                "Register",
                arguments: new QueryArguments
                {
                new QueryArgument<NonNullGraphType<AuthenticationInputType>> { Name = AUTH }
                },
                resolve: async context =>
                {
                    var auth = context.GetArgument<Authentication>(AUTH);

                    var user = await userService.CreateUser(auth);
                    await dbTransaction.CompleteAsync();
                    return user;
                });

             // Coding skills
            FieldAsync<CodingSkillType>(
                "AddSkill",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<CodingSkillInputType>> { Name = SKILL }
                },
                resolve: async context =>
                {
                    var skill = context.GetArgument<CodingSkillEntity>(SKILL);

                    skill = await codingSkillService.Add(skill, (context.UserContext as ClaimsPrincipal).GetId());
                    await dbTransaction.CompleteAsync();
                    return skill;
                });
            FieldAsync<IdGraphType>(
                "DeleteSkill",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID }
                },
                resolve: async context =>
                {
                    var id = context.GetArgument<long>(ID);

                    await codingSkillService.Delete(id);
                    await dbTransaction.CompleteAsync();
                    return id;
                });
            FieldAsync<CodingSkillType>(
                "UpdateSkill",
                arguments: new QueryArguments
                {
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID },
                    new QueryArgument<NonNullGraphType<CodingSkillInputType>> { Name = SKILL }
                },
                resolve: async context =>
                {
                    var id = context.GetArgument<long>(ID);
                    var skill = context.GetArgument<CodingSkillEntity>(SKILL);

                    await codingSkillService.Update(id, skill);
                    await dbTransaction.CompleteAsync();
                    return skill;
                });                        
        }
    }
}
