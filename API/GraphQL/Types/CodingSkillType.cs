using FeatureLibrary.Models.Entities;
using GraphQL.Types;

namespace API.GraphQL.Types
{
    /// <summary>
    /// Coding skill type
    /// </summary>
    public class CodingSkillType : ObjectGraphType<CodingSkillEntity>
    {
        public CodingSkillType()
        {
            Name = "CodingSkill";
            Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>)).Description("Coding skill Id");
            Field(x => x.Name).Description("Coding skill name");
            Field(x => x.Level, type: typeof(NonNullGraphType<CodingSkillLevelType>)).Description("Coding skill level");
        }
    }
}
