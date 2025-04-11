using System.ComponentModel.DataAnnotations;
using static Avatar.Framework.Enums.Enums;

namespace Avatar.Framework.ApiCommand.TeamMemberSkill.Request;

public class UpdateTeamMemberSkillArgs
{
    [Required]
    public int Id { get; set; }
    public int TeamMemberId { get; set; }
    public int SkillId { get; set; }
    public ProficiencyLevel Level { get; set; }
}
