using static Avatar.Framework.Enums.Enums;

namespace Avatar.Framework.ApiCommand.DTO
{
    public class TeamMemberSkillDTO
    {
        public int Id { get; set; }
        public int TeamMemberId { get; set; }
        public int SkillId { get; set; }
        public ProficiencyLevel Level { get; set; }
        public TeamMemberDTO TeamMember { get; set; }
        public SkillDTO Skill { get; set; }
    }
}
