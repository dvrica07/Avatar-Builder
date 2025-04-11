using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.Skill.Request;
public class UpdateSkillArgs
{
    [Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
