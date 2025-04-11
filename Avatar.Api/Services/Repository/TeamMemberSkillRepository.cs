using AutoMapper;
using Avatar.Api.Repository.Interfaces;
using Avatar.Api.Services.Interfaces;
using Avatar.Api.Repository.Entities;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Repository
{
    public class TeamMemberSkillRepository : ITeamMemberSkillRepository
    {
        private readonly IDataStore dataStore;
        private readonly IMapper mapper;
        public TeamMemberSkillRepository(IDataStore dataStore, IMapper mapper)
        {
            this.dataStore = dataStore;
            this.mapper = mapper;
        }

        public async Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetAllAsync()
        {
            try
            {
                var result = await dataStore.TeamMemberSkill.GetAllAsync();
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var skillDTO = mapper.Map<IEnumerable<TeamMemberSkillDTO>>(result.Result);
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateSucceeded(skillDTO, "All team members skills successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(ex, "An error occurred when retrieving all team members skills");
            }
        }
        public async Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetAllTeamMembersSkill(int? count, int? skip)
        {
            try
            {
                var result = await dataStore.TeamMemberSkill.FindAsync(i => true, count, skip);
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var teamMemberSkillDTO = mapper.Map<IEnumerable<TeamMemberSkillDTO>>(result.Result);
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateSucceeded(teamMemberSkillDTO, "Team member dkill successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(ex, "An error occured when retrieving team member skill");
            }
        }
        public async Task<AppResult<IEnumerable<TeamMemberSkillDTO>>> GetTeamMemberSkillById(int id)
        {
            try
            {
                var result = await dataStore.TeamMemberSkill.FindAsync(tms => tms.SkillId == id);

                if (result == null)
                {
                    return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(null, $"Team member skill with ID {id} not found");
                }

                var teamMemberDTO = mapper.Map<IEnumerable<TeamMemberSkillDTO>>(result.Result);
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateSucceeded(teamMemberDTO, "Team member skill successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<TeamMemberSkillDTO>>.CreateFailed(ex, $"An error occurred when retrieving team member skill with ID {id}");
            }
        }
        public async Task<AppResult<TeamMemberSkillDTO>> CreateTeamMemberSkill(TeamMemberSkillDTO teamMemberSkillDTO)
        {
            try
            {
                var teamMemberSkill = mapper.Map<TeamMemberSkill>(teamMemberSkillDTO);
                var result = await dataStore.TeamMemberSkill.Add(teamMemberSkill);
                if (!result.Succeeded || result.Result is null)
                {
                    return AppResult<TeamMemberSkillDTO>.CreateFailed(new ApplicationException(result.Message), result.Message);
                }
                var dtoSkill = mapper.Map<TeamMemberSkillDTO>(result.Result);
                return AppResult<TeamMemberSkillDTO>.CreateSucceeded(dtoSkill, "Team member successfully created");
            }
            catch (Exception ex)
            {
                return AppResult<TeamMemberSkillDTO>.CreateFailed(ex, "An error occured when creating team member");
            }
        }
        public async Task<AppResult<TeamMemberSkillDTO>> UpdateTeamMemberSkill(TeamMemberSkillDTO teamMemberSkillDTO)
        {
            try
            {
                var teamMemberSkillRes = await dataStore.TeamMemberSkill.GetByIdAsync(teamMemberSkillDTO.Id);
                if (!teamMemberSkillRes.Succeeded || teamMemberSkillRes.Result == null)
                {
                    return AppResult<TeamMemberSkillDTO>.CreateFailed(teamMemberSkillRes.Error.Exception, teamMemberSkillRes.Message);
                }
                var teamMemberSkill = teamMemberSkillRes.Result;

                teamMemberSkill.TeamMemberId = teamMemberSkillDTO.TeamMemberId;
                teamMemberSkill.SkillId = teamMemberSkillDTO.SkillId;
                teamMemberSkill.Level = teamMemberSkillDTO.Level;


                var updatedTeamMemberSkill = await dataStore.TeamMemberSkill.Update(teamMemberSkill);
                if (!updatedTeamMemberSkill.Succeeded || updatedTeamMemberSkill.Result is null)
                {
                    return AppResult<TeamMemberSkillDTO>.CreateFailed(new ApplicationException(updatedTeamMemberSkill.Message), updatedTeamMemberSkill.Message);
                }
                var dtoTeamMemberSkill = mapper.Map<TeamMemberSkillDTO>(updatedTeamMemberSkill.Result);
                return AppResult<TeamMemberSkillDTO>.CreateSucceeded(dtoTeamMemberSkill, "Team member skill successfully updated");
            }
            catch (Exception ex)
            {
                return AppResult<TeamMemberSkillDTO>.CreateFailed(ex, "An error occured when updating team member skill");
            }
        }
        public async Task<AppResult<bool>> DeleteTeamMemberSkill(int Id)
        {
            var existingTeamMemberSkill = await dataStore.TeamMemberSkill.GetByIdAsync(Id);
            if (!existingTeamMemberSkill.Succeeded || existingTeamMemberSkill.Result == null)
            {
                return AppResult<bool>.CreateFailed(existingTeamMemberSkill.Error?.Exception ?? new KeyNotFoundException(), existingTeamMemberSkill.Message ?? $"Skill with ID {Id} not found");
            }

            var result = await dataStore.TeamMemberSkill.Remove(existingTeamMemberSkill.Result);
            if (!result.Succeeded)
            {
                return AppResult<bool>.CreateFailed(result.Error?.Exception, result.Message ?? $"Failed to delete team member skill with ID {Id}");
            }

            return AppResult<bool>.CreateSucceeded(true, $"Team member skill with ID {Id} was successfully deleted");
        }

    }
}
