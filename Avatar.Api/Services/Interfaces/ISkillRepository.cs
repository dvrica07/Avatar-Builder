using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Interfaces
{
    public interface ISkillRepository
    {
        Task<AppResult<IEnumerable<SkillDTO>>> GetAllSkills(int? count, int? skip);
        Task<AppResult<IEnumerable<SkillDTO>>> GetAllAsync();
        Task<AppResult<SkillDTO>> CreateSkill(SkillDTO skillDTO);
        Task<AppResult<SkillDTO>> UpdateSkill(SkillDTO skillDTO);
        Task<AppResult<bool>> DeleteSkill(int Id);
        Task<AppResult<SkillDTO>> GetSkillById(int id);
    }
}
