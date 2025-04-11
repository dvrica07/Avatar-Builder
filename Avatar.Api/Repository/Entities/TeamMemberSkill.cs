using static Avatar.Framework.Enums.Enums;

namespace Avatar.Api.Repository.Entities
{
    public class TeamMemberSkill : BaseEntity
    {
        public int TeamMemberId { get; set; }
        public int SkillId { get; set; }
        public ProficiencyLevel Level { get; set; }
        public TeamMember TeamMember { get; set; }
        public Skill Skill { get; set; }    
    }
}
