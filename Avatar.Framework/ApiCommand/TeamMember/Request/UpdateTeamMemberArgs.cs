using Avatar.Framework.ApiCommand.DTO;
using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.TeamMember.Request;
public class UpdateTeamMembeArgs
{
    [Required]
    public int Id { get; set; }
    public string FirstName { get; set; }     
    public string LastName { get; set; }    
    public string Title { get; set; }
    public string FullName { get { return FirstName + " " + LastName; } }
}
