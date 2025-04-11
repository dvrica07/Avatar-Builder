using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Interfaces;

public interface ITeamMemberSkillRepository
{
    Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetAllTeamMembersSkill(int? count, int? skip);
    Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetAllAsync();
    Task<AppResult<TeamMemberSkillDTO>> CreateTeamMemberSkill(TeamMemberSkillDTO teamMemberSkillDTO);
    Task<AppResult<TeamMemberSkillDTO>> UpdateTeamMemberSkill(TeamMemberSkillDTO teamMemberSkillDTO);
    Task<AppResult<bool>> DeleteTeamMemberSkill(int Id);
    Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetTeamMemberSkillById(int id);
}
