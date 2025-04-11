using Avatar.Framework.ApiCommand.DTO;

namespace Avatar.Api.Repository.Entities
{
    public class TeamMember : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string FullName { get { return FirstName +" " + LastName; } }
    }
}
