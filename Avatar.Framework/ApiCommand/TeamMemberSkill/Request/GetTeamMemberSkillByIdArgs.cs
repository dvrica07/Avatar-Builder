using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.TeamMemberSkill.Request;

public class GetTeamMemberSkillByIdArgs
{
    [Required]
    public int SkillId { get; set; }
}
