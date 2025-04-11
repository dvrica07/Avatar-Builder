using Avatar.Api.Repository.Entities;
using Avatar.Api.Repository.Interfaces;

namespace Avatar.Api.Repository.DbSets
{
    public class TeamMemberEntity : GenericEntity<TeamMember> , ITeamMember
    {
        public TeamMemberEntity(ApplicationContext application) : base(application)
        {
        }
    }   
}
