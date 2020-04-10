using FeatureLibrary.Models.Entities;
using HotChocolate.Types.Sorting;

namespace API.GraphQL.Types
{
    
    /// <summary>
    /// Coding Skill Sort type
    /// </summary>
    public class CodingSkillSortType : SortInputType<CodingSkillEntity>
    {
        protected override void Configure(ISortInputTypeDescriptor<CodingSkillEntity> descriptor)
        {
            Name = "CodingSkillSort";
            descriptor.BindFieldsExplicitly();
            descriptor.Sortable(S => S.Id);
            descriptor.Sortable(s => s.Name);
            descriptor.Sortable(s => s.Level);
        }
    }
}
