namespace Avatar.Framework.ApiCommand.DTO;

public class TeamMemberDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string FullName { get { return FirstName + " " + LastName; } }
}
