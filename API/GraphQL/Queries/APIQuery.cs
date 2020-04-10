using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using API.GraphQL.Types;
using FeatureLibrary.Extensions;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Repositories;
using FeatureLibrary.Services;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace API.GraphQL.Queries
{
    public class APIQuery : ObjectType
    {
        #region Query consts
        private const string ID = "id";
        private const string NAME = "name";
        private const string LEVELS = "levels";
        #endregion

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            /*
             * Users
             */
            descriptor
                .Field("User")
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("User Id"))
                .Type<UserType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<IUserRepository>().GetById(ctx.Argument<long>(ID));
                });
            descriptor
                .Field("Users")
                .Type<ListType<UserType>>()
                .Resolver(ctx =>
                {
                    return ctx.Service<IUserRepository>().GetAsQueryable();
                })
                .UseFiltering<UserFilterType>()
                .UseSorting<UserSortType>();
            //// Users
            //FieldAsync<UserType>(
            //    Name = "User",
            //    arguments: new QueryArguments {
            //        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID, Description = "User Id"}
            //    },
            //    resolve: async context => {
            //        return await userService.GetById(context.GetArgument<long>(ID));
            //    });
            //FieldAsync<ListGraphType<UserType>>(
            //    Name = "Users",
            //    resolve: async context => {
            //        return await userService.GetAll();
            //    });

            //// Coding skills
            //FieldAsync<CodingSkillType>(
            //    Name = "CodingSkill",
            //    arguments: new QueryArguments {
            //        new QueryArgument<NonNullGraphType<IdGraphType>> { Name = ID, Description = "Coding skill Id" }
            //    },
            //    resolve: async context => {
            //        return await codingSkillService.GetById(context.GetArgument<long>(ID));
            //    });
            //FieldAsync<ListGraphType<CodingSkillType>>(
            //    Name = "CodingSkills",
            //     arguments: new QueryArguments {
            //        new QueryArgument<StringGraphType> { Name = NAME, Description = "Coding skill name"},
            //        new QueryArgument<ListGraphType<CodingSkillLevelType>> { Name = LEVELS, Description = "Coding skill level"}
            //    },
            //    resolve: async context => {
            //        var filter = new CodingSkillFilter
            //        {
            //            Name = context.GetArgument<string>(NAME),
            //            Levels = context.GetArgument<List<CodingSkillLevel>>(LEVELS)
            //        };
            //        return await codingSkillService.GetByFilter(filter);
            //    });
        }
    }
}
