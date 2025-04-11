using Avatar.Api.Repository.DbSets;
using Avatar.Api.Repository.Interfaces;
using Avatar.Api.Repository;

namespace Avatar.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ExtendServices(this IServiceCollection services)
    {

        services.AddTransient(typeof(IGenericEntity<>), typeof(GenericEntity<>));
        services.AddTransient<IDataStore, DataStore>();
        services.AddTransient<ISkill, SkillEntity>();
        services.AddTransient<ITeamMemberSkill, TeamMemberSkillEntity>();
        services.AddTransient<ITeamMember, TeamMemberEntity>();

        services.AddTransient<Services.Interfaces.ISkillRepository, Services.Repository.SkillRepository>();
        services.AddTransient<Services.Interfaces.ITeamMemberRepository, Services.Repository.TeamMemberRepository>();
        services.AddTransient<Services.Interfaces.ITeamMemberSkillRepository, Services.Repository.TeamMemberSkillRepository>();

        return services;
    }
}
