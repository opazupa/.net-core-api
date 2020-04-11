using FeatureLibrary.Models.Entities;
using HotChocolate.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// Coding skill type
    /// </summary>
    public class CodingSkillType : ObjectType<CodingSkillEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<CodingSkillEntity> descriptor)
        {
            Name = "CodingSkill";

            descriptor
                .Field(x => x.Id)
                .Description("Coding skill Id");

            descriptor
                .Field(x => x.Name)
                .Type<NonNullType<StringType>>()
                .Description("Coding skill name");

            descriptor
                .Field(x => x.Level)
                .Type<NonNullType<CodingSkillLevelType>>()
                .Description("Coding skill level");
        }
    }
}
