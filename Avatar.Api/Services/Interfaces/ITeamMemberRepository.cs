using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Interfaces
{
    public interface ITeamMemberRepository
    {
        Task<AppResult<IEnumerable<TeamMemberDTO>>> GetAllTeamMembers(int? count, int? skip);
        Task<AppResult<IEnumerable<TeamMemberDTO>>> GetAllAsync();
        Task<AppResult<TeamMemberDTO>> CreateTeamMember(TeamMemberDTO teamMemberDTO);
        Task<AppResult<TeamMemberDTO>> UpdateTeamMember(TeamMemberDTO teamMemberDTO);
        Task<AppResult<bool>> DeleteTeamMember(int Id);
        Task<AppResult<TeamMemberDTO>> GetTeamMemberById(int id);
        Task<AppResult<bool>>IsTeamMemberExists(int memberId);
    }
}
