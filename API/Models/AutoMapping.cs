using AutoMapper;
using FeatureLibrary.Models.Entities;

namespace API.Models
{
    /// <summary>
    /// Profile for automapping properties
    /// </summary>
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<CodingSkillEntity, CodingSkill>().ReverseMap();

            CreateMap<NewSkill, CodingSkillEntity>();
            CreateMap<ModifiedSkill, CodingSkillEntity>();
        }
    }
}
