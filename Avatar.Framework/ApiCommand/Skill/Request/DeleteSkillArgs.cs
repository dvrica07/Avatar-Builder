using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.Skill.Request;
public class DeleteSkillArgs
{
    [Required]
    public int Id { get; set; }
}
