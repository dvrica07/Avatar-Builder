using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.Skill.Request;
public class CreateSkillArgs
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}
