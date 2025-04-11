namespace Avatar.Web.Extensions;

public static class ExtensionService
{
    public static IServiceCollection AppExtendServices(this IServiceCollection services)
    {
        //Add services extension
        services.AddTransient<Modules.ApiAccess.Handlers.ITeamMemberApiHandler, Modules.ApiAccess.TeamMember.TeamMemberApihandler>();
        services.AddTransient<Modules.ApiAccess.Handlers.ISkillApiHandler, Modules.ApiAccess.Skill.SkillApiHandler>();
        services.AddTransient<Modules.ApiAccess.Handlers.ITeamMemberSkillApiHandler, Modules.ApiAccess.TeamMemberSkill.TeamMemberSkillApiHandler>();

        services.AddHttpContextAccessor();
        services.AddScoped(sp => sp.GetService<IHttpContextAccessor>().HttpContext?.User);

        return services;
    } 
}