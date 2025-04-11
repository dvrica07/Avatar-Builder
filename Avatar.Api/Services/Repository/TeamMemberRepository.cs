using AutoMapper;
using Avatar.Api.Repository.Interfaces;
using Avatar.Api.Services.Interfaces;
using Avatar.Api.Repository.Entities;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.Common;

namespace Avatar.Api.Services.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly IDataStore dataStore;
        private readonly IMapper mapper;
        public TeamMemberRepository(IDataStore dataStore, IMapper mapper)
        {
            this.dataStore = dataStore;
            this.mapper = mapper;
        }

        public async Task<AppResult<IEnumerable<TeamMemberDTO>>> GetAllTeamMembers(int? count, int? skip)
        {
            try
            {
                var result = await dataStore.TeamMember.FindAsync(i => true, count, skip);
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<TeamMemberDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var teamMemberDTO = mapper.Map<IEnumerable<TeamMemberDTO>>(result.Result);
                return AppResult<IEnumerable<TeamMemberDTO>>.CreateSucceeded(teamMemberDTO, "Team Member successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<TeamMemberDTO>>.CreateFailed(ex, "An error occured when retrieving team member");
            }
        }
        public async Task<AppResult<IEnumerable<TeamMemberDTO>>> GetAllAsync()
        {
            try
            {
                var result = await dataStore.TeamMember.GetAllAsync();
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<IEnumerable<TeamMemberDTO>>.CreateFailed(result.Error.Exception, result.Message);
                }
                var teamMemberDTO = mapper.Map<IEnumerable<TeamMemberDTO>>(result.Result);
                return AppResult<IEnumerable<TeamMemberDTO>>.CreateSucceeded(teamMemberDTO, "All team members successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<IEnumerable<TeamMemberDTO>>.CreateFailed(ex, "An error occurred when retrieving all team members");
            }
        }
        public async Task<AppResult<TeamMemberDTO>> GetTeamMemberById(int id)
        {
            try
            {
                var result = await dataStore.TeamMember.GetByIdAsync(id);
                if (!result.Succeeded || result.Result == null)
                {
                    return AppResult<TeamMemberDTO>.CreateFailed(result.Error.Exception, result.Message);
                }
                var teamMemberDTO = mapper.Map<TeamMemberDTO>(result.Result);
                return AppResult<TeamMemberDTO>.CreateSucceeded(teamMemberDTO, "Team member successfully retrieved");
            }
            catch (Exception ex)
            {
                return AppResult<TeamMemberDTO>.CreateFailed(ex, $"An error occurred when retrieving team member with ID {id}");
            }
        }
        public async Task<AppResult<TeamMemberDTO>> CreateTeamMember(TeamMemberDTO teamMemberDTO)
        {
            try
            {
                var teamMember = mapper.Map<TeamMember>(teamMemberDTO);
                var result = await dataStore.TeamMember.Add(teamMember);
                if (!result.Succeeded || result.Result is null)
                {
                    return AppResult<TeamMemberDTO>.CreateFailed(new ApplicationException(result.Message), result.Message);
                }
                var dtoSkill = mapper.Map<TeamMemberDTO>(result.Result);
                return AppResult<TeamMemberDTO>.CreateSucceeded(dtoSkill, "Team member successfully created");
            }
            catch (Exception ex)
            {
                return AppResult<TeamMemberDTO>.CreateFailed(ex, "An error occured when creating team member");
            }
        }
        public async Task<AppResult<TeamMemberDTO>> UpdateTeamMember(TeamMemberDTO teamMemberDTO)
        {
            try
            {
                // Get existing team member
                var teamMemberRes = await dataStore.TeamMember.GetByIdAsync(teamMemberDTO.Id);
                if (!teamMemberRes.Succeeded || teamMemberRes.Result == null)
                {
                    return AppResult<TeamMemberDTO>.CreateFailed(teamMemberRes.Error.Exception, teamMemberRes.Message);
                }
                var teamMember = teamMemberRes.Result;

                // Update properties from parameters
                teamMember.FirstName = teamMemberDTO.FirstName;
                teamMember.LastName = teamMemberDTO.LastName;
                teamMember.Title = teamMemberDTO.Title;

                // Save changes
                var updatedTeamMember = await dataStore.TeamMember.Update(teamMember);
                if (!updatedTeamMember.Succeeded || updatedTeamMember.Result is null)
                {
                    return AppResult<TeamMemberDTO>.CreateFailed(new ApplicationException(updatedTeamMember.Message), updatedTeamMember.Message);
                }
                var dtoTeamMember = mapper.Map<TeamMemberDTO>(updatedTeamMember.Result);
                return AppResult<TeamMemberDTO>.CreateSucceeded(dtoTeamMember, "Team member successfully updated");
            }
            catch (Exception ex)
            {
                return AppResult<TeamMemberDTO>.CreateFailed(ex, "An error occurred when updating team member");
            }
        }
        public async Task<AppResult<bool>> DeleteTeamMember(int Id)
        {
            try
            {
                var existingTeamMember = await dataStore.TeamMember.GetByIdAsync(Id);
                if (!existingTeamMember.Succeeded || existingTeamMember.Result == null)
                {
                    return AppResult<bool>.CreateFailed(existingTeamMember.Error?.Exception ?? new KeyNotFoundException(), existingTeamMember.Message ?? $"Skill with ID {Id} not found");
                }

                var result = await dataStore.TeamMember.Remove(existingTeamMember.Result);
                if (!result.Succeeded)
                {
                    return AppResult<bool>.CreateFailed(result.Error?.Exception, result.Message ?? $"Failed to delete team member with ID {Id}");
                }

                return AppResult<bool>.CreateSucceeded(true, $"Team member with ID {Id} was successfully deleted");
            }
            catch (Exception ex)
            {
                return AppResult<bool>.CreateFailed(ex, $"An error occurred when deleting team member with ID {Id}");
            }
        }
    }
}
