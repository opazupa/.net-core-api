using API.GraphQL.Types.Enums;
using FeatureLibrary.Models.Entities;
using HotChocolate.Types;

namespace API.GraphQL.Types.Inputs
{
   /// <summary>
   /// Coding Skill input type
   /// </summary>
   public class CodingSkillInputType : InputObjectType<CodingSkillEntity>
   {
        protected override void Configure(IInputObjectTypeDescriptor<CodingSkillEntity> descriptor)
       {
            Name = "CodingSkillInput";
            descriptor.BindFieldsExplicitly();

            descriptor.Field(x => x.Name)
                .Description("Coding skill name");

            descriptor.Field(x => x.Level)
                .Type<NonNullType<CodingSkillLevelType>>()
                .Description("Coding skill level");
       }
   }
}
