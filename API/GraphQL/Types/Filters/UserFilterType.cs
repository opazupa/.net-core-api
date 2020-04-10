using FeatureLibrary.Models.Entities;
using HotChocolate.Types.Filters;

namespace API.GraphQL.Types
{   
    /// <summary>
    /// User filter type
    /// </summary>
    public class UserFilterType : FilterInputType<UserEntity>
    {
        protected override void Configure(IFilterInputTypeDescriptor<UserEntity> descriptor)
        {
            Name = "UserFilter";
            descriptor.BindFieldsExplicitly();
            descriptor.Filter(u => u.UserName);
            descriptor.Filter(u => u.Id);
        }
    }
}
