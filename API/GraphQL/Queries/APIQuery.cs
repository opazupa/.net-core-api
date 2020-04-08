using System.Collections.Generic;
using FeatureLibrary.Models;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using GraphQL.Types;

namespace API.GraphQL
{
    public class APIQuery : ObjectGraphType
    {
        #region Query consts
        private const string ID = "id";
        private const string NAME = "name";
        private const string LEVELS = "levels";
        #endregion

        /// <summary>
        /// API queries
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="codingSkillService"></param>
        public APIQuery(IUserService userService, ICodingSkillService codingSkillService)
        {
            // Users
            FieldAsync<UserType>(
                Name = "User",
                arguments: new QueryArguments {
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID, Description = "User Id"}
                },
                resolve: async context => {
                    return await userService.GetById(context.GetArgument<long>(ID));
                });
            FieldAsync<ListGraphType<UserType>>(
                Name = "Users",
                resolve: async context => {
                    return await userService.GetAll();
                });

            // Coding skills
            FieldAsync<CodingSkillType>(
                Name = "CodingSkill",
                arguments: new QueryArguments {
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID, Description = "Coding skill Id" }
                },
                resolve: async context => {
                    return await codingSkillService.GetById(context.GetArgument<long>(ID));
                });
            FieldAsync<ListGraphType<CodingSkillType>>(
                Name = "CodingSkills",
                 arguments: new QueryArguments {
                    new QueryArgument<StringGraphType> { Name = NAME, Description = "Coding skill name"},
                    new QueryArgument<ListGraphType<CodingSkillLevelType>> { Name = LEVELS, Description = "Coding skill level"}
                },
                resolve: async context => {
                    var filter = new CodingSkillFilter
                    {
                        Name = context.GetArgument<string>(NAME),
                        Levels = context.GetArgument<List<CodingSkillLevel>>(LEVELS)
                    };
                    return await codingSkillService.GetByFilter(filter);
                });
        }
    }
}
