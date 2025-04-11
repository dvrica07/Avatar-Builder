namespace Avatar.Api.Repository.Interfaces
{
    public interface IDataStore
    {
        ISkill Skill { get; }
        ITeamMemberSkill TeamMemberSkill { get; }    
        ITeamMember TeamMember { get; }
        Task EnsureMigrate();
    }
}
