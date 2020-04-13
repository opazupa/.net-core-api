using System.Linq;
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
            descriptor.Authorize();
            /*
             * Users
             */
            descriptor
                .Field("user")
                .Argument(ID, arg => arg.Type<NonNullType<IdType>>().Description("User Id"))
                .Type<UserType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<IUserRepository>().GetById(ctx.Argument<long>(ID));
                });
            descriptor
                .Field("users")
                .Type<ListType<UserType>>()
                .Resolver(async ctx =>
                {
                    return (await ctx.Service<IUserRepository>().GetAll()).AsQueryable();
                })
                .UseFiltering<UserFilterType>()
                .UseSorting<UserSortType>();

            /*
            *  Coding skills
            */
            descriptor
                .Field("codingSkill")
                .Argument(ID, arg => arg.Type<NonNullType<IdType>>().Description("Coding skill Id"))
                .Type<CodingSkillType>()
                .Resolver(async ctx =>
                {
                    return await ctx.Service<ICodingSkillRepository>().GetById(ctx.Argument<long>(ID));
                });
            descriptor
                .Field("codingSkills")
                .Type<ListType<CodingSkillType>>()
                .Resolver(async ctx =>
                {
                    return (await ctx.Service<ICodingSkillRepository>().GetAll()).AsQueryable();
                })
                .UseFiltering<CodingSkillFilterType>()
                .UseSorting<CodingSkillSortType>();
        }
    }
}
