using System.Threading;
using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// User type
    /// </summary>
    public class UserType : ObjectType<UserEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<UserEntity> descriptor)
        {
            Name = "User";
            descriptor.Field(x => x.Id)
                .Type<NonNullType<IdType>>()
                .Description("User Id");
            descriptor.Field(x => x.UserName)
                .Description("Username");
            descriptor.Field(x => x.Skills)
                .Type<ListType<CodingSkillType>>()
                .Description("User coding skills")
                .Resolver(async ctx =>
                {
                    var loader = ctx.GroupDataLoader<long, CodingSkillEntity>("GetSkillsByUserIds", keys => ctx.Service<ICodingSkillService>().GetByUserIds(keys));
                    return await loader.LoadAsync(ctx.Parent<UserEntity>().Id, new CancellationToken());
                });
        }
    }
}
