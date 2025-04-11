using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.TeamMemberSkill.Request
{
    public class DeleteTeamMemberSkillArgs
    {
        [Required]
        public int Id { get; set; }
    }
}
