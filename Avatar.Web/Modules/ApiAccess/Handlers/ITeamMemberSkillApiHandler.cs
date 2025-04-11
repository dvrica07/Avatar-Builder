using Avatar.Framework.ApiCommand.TeamMemberSkill.Request;
using Avatar.Framework.ApiCommand.TeamMemberSkill.Response;
using Avatar.Framework.Common;

namespace Avatar.Web.Modules.ApiAccess.Handlers;
public interface ITeamMemberSkillApiHandler
{
    Task<AppResult<GetAllTeamMemberSkillResult>> GetTeamMemberSkillList();
    Task<AppResult<GetTeamMemberSkillByIdResult>> GetTeamMemberSkillById(GetTeamMemberSkillByIdArgs args);
    Task<AppResult<CreateTeamMemberSkillResult>> CreateTeamMemberSkill(CreateTeamMemberSkillArgs args);
    Task<AppResult<UpdateTeamMemberSkillResult>> UpdateTeamMemberSkill(UpdateTeamMemberSkillArgs args);
    Task<AppResult<DeleteTeamMemberSkillResult>> DeleteTeamMemberSkill(DeleteTeamMemberSkillArgs args);
}
