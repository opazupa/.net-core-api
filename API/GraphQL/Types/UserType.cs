using FeatureLibrary.Models.Entities;
using FeatureLibrary.Services;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// User type
    /// </summary>
    public class UserType : ObjectGraphType<UserEntity>
    {
        public UserType(ICodingSkillService codingSkillService, IDataLoaderContextAccessor dataLoader)
        {
            Name = "User";
            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("User Id");
            Field(x => x.UserName).Description("Username");
            Field(x => x.Skills, type: typeof(ListGraphType<CodingSkillType>))
                .Description("User coding skills")
                .ResolveAsync(context => {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<long, CodingSkillEntity>("GetSkillsByUserIds", codingSkillService.GetSkillsByUserIds);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
