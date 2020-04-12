using FeatureLibrary.Models.Entities;
using HotChocolate.Types.Sorting;

namespace API.GraphQL.Types.Sorts
{
    
    /// <summary>
    /// User Sort type
    /// </summary>
    public class UserSortType : SortInputType<UserEntity>
    {
        protected override void Configure(ISortInputTypeDescriptor<UserEntity> descriptor)
        {
            Name = "UserSort";
            descriptor.BindFieldsExplicitly();
            descriptor.Sortable(u => u.Id);
            descriptor.Sortable(u => u.UserName);
        }
    }
}
