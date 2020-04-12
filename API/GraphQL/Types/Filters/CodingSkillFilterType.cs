using FeatureLibrary.Models.Entities;
using HotChocolate.Types.Filters;

namespace API.GraphQL.Types.Filters
{   
    /// <summary>
    /// Coding Skill filter type
    /// </summary>
    public class CodingSkillFilterType : FilterInputType<CodingSkillEntity>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CodingSkillEntity> descriptor)
        {
            Name = "CodingSkillFilter";
            descriptor.BindFieldsExplicitly();
            descriptor.Filter(s => s.Name);
            descriptor.Filter(s => s.Level);
            descriptor.Filter(s => s.Id);
            descriptor.Filter(s => s.UserId);
        }
    }
}
