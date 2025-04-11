using Avatar.Api.Repository.Entities;
using Avatar.Api.Repository.Interfaces;

namespace Avatar.Api.Repository.DbSets;

public class TeamMemberSkillEntity : GenericEntity<TeamMemberSkill> , ITeamMemberSkill
{
    public TeamMemberSkillEntity(ApplicationContext application) : base(application)
    {
    }
}
