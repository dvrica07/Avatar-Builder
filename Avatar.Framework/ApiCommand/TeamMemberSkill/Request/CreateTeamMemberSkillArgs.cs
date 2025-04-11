using System.ComponentModel.DataAnnotations;
using static Avatar.Framework.Enums.Enums;

namespace Avatar.Framework.ApiCommand.TeamMemberSkill.Request;

public class CreateTeamMemberSkillArgs
{
    [Required]
    public int TeamMemberId { get; set; }
    [Required]
    public int SkillId { get; set; }
    [Required]
    public ProficiencyLevel Level { get; set; }
}
