using AutoMapper;
using Avatar.Api.Repository.Interfaces;
using Avatar.Api.Services.Interfaces;
using Avatar.Api.Repository.Entities;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly IDataStore dataStore;
        private readonly IMapper mapper;    
        public SkillRepository(IDataStore dataStore, IMapper mapper )
        {
            this.dataStore = dataStore;
            this.mapper = mapper;
        }

        //With Pagination
        public async Task<AppResult<IEnumerable<SkillDTO>>> GetAllSkills(int? count, int? skip)
        {
            try
            {
                var result = await dataStore.Skill.FindAsync(i => true, count, skip);
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<SkillDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var skillDTO = mapper.Map<IEnumerable<SkillDTO>>(result.Result);
                return AppResult<IEnumerable<SkillDTO>>.CreateSucceeded(skillDTO, "Skills successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<SkillDTO>>.CreateFailed(ex, "An error occured when retrieving skills");
            }
        }
        public async Task<AppResult<IEnumerable<SkillDTO>>> GetAllAsync()
        {
            try
            {
                var result = await dataStore.Skill.GetAllAsync();
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<SkillDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var skillDTO = mapper.Map<IEnumerable<SkillDTO>>(result.Result);
                return AppResult<IEnumerable<SkillDTO>>.CreateSucceeded(skillDTO, "All skills successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<SkillDTO>>.CreateFailed(ex, "An error occurred when retrieving all skills");
            }
        }
        public async Task<AppResult<SkillDTO>> GetSkillById(int id)
        {
            try
            {
                var result =await dataStore.Skill.GetByIdAsync(id);
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<SkillDTO>.CreateFailed(result.Error.Exception, result.Message);
                }
                var skillDTO = mapper.Map<SkillDTO>(result.Result);
                return AppResult<SkillDTO>.CreateSucceeded(skillDTO, "Skill waitlist successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<SkillDTO>.CreateFailed(ex, $"An error occurred when retrieving skill waitlist with ID {id}");
            }
        }
        public async Task<AppResult<SkillDTO>> CreateSkill(SkillDTO skillDTO)
        {
            try
            {
                var skills = mapper.Map<Skill>(skillDTO);
                var result = await dataStore.Skill.Add(skills);
                if (!result.Succeeded || result.Result is null)
                {
                    return AppResult<SkillDTO>.CreateFailed(new ApplicationException(result.Message), result.Message);
                }
                var dtoSkill = mapper.Map<SkillDTO>(result.Result);
                return AppResult<SkillDTO>.CreateSucceeded(dtoSkill, "Skill successfully created");
            }
            catch (Exception ex)
            {
                return AppResult<SkillDTO>.CreateFailed(ex, "An error occured when creating skill");
            }
        }
        public async Task<AppResult<SkillDTO>> UpdateSkill(SkillDTO skillDTO)
        {
            try
            {
                var skillRes = await dataStore.Skill.GetByIdAsync(skillDTO.Id);
                if (!skillRes.Succeeded || skillRes.Result == null)
                {
                    return AppResult<SkillDTO>.CreateFailed(skillRes.Error.Exception, skillRes.Message);
                }
                //Update skill 
                var skill = skillRes.Result;
                skill.Name = skillDTO.Name;
                skill.Description = skillDTO.Description;

                var updatedSkill = await dataStore.Skill.Update(skill);
                if (!updatedSkill.Succeeded || updatedSkill.Result is null)
                {
                    return AppResult<SkillDTO>.CreateFailed(new ApplicationException(updatedSkill.Message), updatedSkill.Message);
                }
                var dtoSkill = mapper.Map<SkillDTO>(updatedSkill.Result);
                return AppResult<SkillDTO>.CreateSucceeded(dtoSkill, "Skill successfully updated");
            }
            catch (Exception ex)
            {
                return AppResult<SkillDTO>.CreateFailed(ex, "An error occured when updating skill");
            }
        }
        public async Task<AppResult<bool>> DeleteSkill(int Id)
        {
            try
            {
                var existingSkill = await dataStore.Skill.GetByIdAsync(Id);
                if (!existingSkill.Succeeded || existingSkill.Result == null)
                {
                    return AppResult<bool>.CreateFailed(existingSkill.Error?.Exception ?? new KeyNotFoundException(), existingSkill.Message ?? $"Skill with ID {Id} not found");
                }

                var result = await dataStore.Skill.Remove(existingSkill.Result);
                if (!result.Succeeded)
                {
                    return AppResult<bool>.CreateFailed(result.Error?.Exception, result.Message ?? $"Failed to delete skill with ID {Id}");
                }

                return AppResult<bool>.CreateSucceeded(true, $"Skill with ID {Id} was successfully deleted");
            }
            catch (Exception ex)
            {
                return AppResult<bool>.CreateFailed(ex, $"An error occurred when deleting skill with ID {Id}");
            }
        }
    }
}
