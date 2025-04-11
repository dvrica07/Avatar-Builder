using Avatar.Api.Repository.Entities;
using Avatar.Api.Repository.Interfaces;

namespace Avatar.Api.Repository.DbSets
{
    public class SkillEntity : GenericEntity<Skill> , ISkill
    {
        public SkillEntity(ApplicationContext applicationContext) : base(applicationContext)
        { 
        }
    }
}
