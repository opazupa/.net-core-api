using API.GraphQL.Types;
using API.GraphQL.Types.Filters;
using API.GraphQL.Types.Sorts;
using FeatureLibrary.Repositories;
using HotChocolate.Types;

namespace API.GraphQL.Queries
{
    /// <summary>
    /// API mutations
    /// </summary>
    public class APIQuery : ObjectType
    {
        #region Query consts
        private const string ID = "id";
        #endregion

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            /*
             * Users
             */
            descriptor
                .Field("user")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("User Id"))
                .Type<UserType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<IUserRepository>().GetById(ctx.Argument<long>(ID));
                });
            descriptor
                .Field("users")
                .Authorize()
                .Type<ListType<UserType>>()
                .Resolver(ctx =>
                {
                    return ctx.Service<IUserRepository>().GetAsQueryable();
                })
                .UseFiltering<UserFilterType>()
                .UseSorting<UserSortType>();

            /*
            *  Coding skills
            */
            descriptor
                .Field("codingSkill")
                .Authorize()
                .Argument(ID, arg => arg.Type<NonNullType<LongType>>().Description("Coding skill Id"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<ICodingSkillRepository>().GetById(ctx.Argument<long>(ID));
                });
            descriptor
                .Field("codingSkills")
                .Authorize()
                .Type<ListType<CodingSkillType>>()
                .Resolver(ctx =>
                {
                    return ctx.Service<ICodingSkillRepository>().GetAsQueryable();
                })
                .UseFiltering<CodingSkillFilterType>()
                .UseSorting<CodingSkillSortType>();
        }
    }
}
