using Avatar.Framework.ApiCommand.Skill.Request;
using Avatar.Framework.ApiCommand.Skill.Response;
using Avatar.Framework.Common;

namespace Avatar.Web.Modules.ApiAccess.Handlers;

public interface ISkillApiHandler
{
    Task<AppResult<GetAllSkillResult>> GetSkillList();
    Task<AppResult<CreateSkillResult>> CreateSkill(CreateSkillArgs args);
    Task<AppResult<UpdateSkillResult>> UpdateSkill(UpdateSkillArgs args);
    Task<AppResult<DeleteSkillResult>> DeleteSkill(DeleteSkillArgs args);
}
