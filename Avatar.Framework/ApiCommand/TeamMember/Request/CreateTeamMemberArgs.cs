using Avatar.Framework.ApiCommand.DTO;
using System.ComponentModel.DataAnnotations;

namespace Avatar.Framework.ApiCommand.TeamMember.Request
{
    public class CreateTeamMemberArgs
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Title { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
