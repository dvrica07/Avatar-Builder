using Avatar.Api.Repository.DbSets;
using Avatar.Api.Repository.Entities;
using Avatar.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avatar.Api.Repository
{
    public class DataStore : IDataStore
    {
        private readonly ApplicationContext applicationContext;
        public DataStore(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;   
        }

        public ISkill Skill => new SkillEntity(applicationContext);

        public ITeamMemberSkill TeamMemberSkill => new TeamMemberSkillEntity(applicationContext);

        public ITeamMember TeamMember => new TeamMemberEntity(applicationContext);

        public async Task EnsureMigrate()
        {
            await applicationContext.Database.MigrateAsync();
        }
    }
}
