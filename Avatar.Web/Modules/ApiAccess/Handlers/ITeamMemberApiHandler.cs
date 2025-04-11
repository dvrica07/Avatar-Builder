using Avatar.Framework.ApiCommand.TeamMember.Request;
using Avatar.Framework.ApiCommand.TeamMember.Response;
using Avatar.Framework.Common;

namespace Avatar.Web.Modules.ApiAccess.Handlers;

public interface ITeamMemberApiHandler
{
    Task<AppResult<GetAllTeamMemberResult>> GetTeamMemberList();
    Task<AppResult<CreateTeamMemberResult>> CreateTeamMember(CreateTeamMemberArgs args);
    Task<AppResult<UpdateTeamMemberResult>> UpdateTeamMember(UpdateTeamMembeArgs args);
    Task<AppResult<DeleteTeamMemberResult>> DeleteTeamMember(DeleteTeamMemberArgs args);
}
