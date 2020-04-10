using FeatureLibrary.Models.Entities;
using GraphQL.Types;

namespace API.GraphQL.Types.Inputs
{
    /// <summary>
    /// Coding skill input type
    /// </summary>
    public class CodingSkillInputType : InputObjectGraphType<CodingSkillEntity>
    {
        public CodingSkillInputType()
        {
            Name = "CodingSkillInput";
            Field(x => x.Name).Description("Skill name");
            Field(x => x.Level, type: typeof(NonNullGraphType<CodingSkillLevelType>)).Description("Skill level");
        }
    }
}
