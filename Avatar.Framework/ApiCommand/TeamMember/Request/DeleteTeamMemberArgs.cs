using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.TeamMember.Request;
public class DeleteTeamMemberArgs
{
    [Required]
    public int Id { get; set; }
}
