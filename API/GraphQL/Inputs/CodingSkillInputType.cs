using API.GraphQL.Types;
using FeatureLibrary.Models.Entities;
using GraphQL.Types;

namespace API.GraphQL.Inputs
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
