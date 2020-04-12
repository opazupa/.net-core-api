using FeatureLibrary.Models.Entities;
using HotChocolate.Types;

namespace API.GraphQL.Types.Enums
{
    /// <summary>
    /// Coding skill level type
    /// </summary>
    public class CodingSkillLevelType : EnumType<CodingSkillLevel> 
    {
        protected override void Configure(IEnumTypeDescriptor<CodingSkillLevel> descriptor)
        {
            descriptor.Value(CodingSkillLevel.GettingThingsDone)
                .Name("GETTING_THINGS_DONE");
        }
    }
}
