using Avatar.Framework.ApiCommand.TeamMemberSkill.Request;
using Avatar.Framework.ApiCommand.TeamMemberSkill.Response;
using Avatar.Framework.Common;
using Avatar.Web.Modules.ApiAccess.Handlers;
using Flurl.Http.Configuration;
using Flurl.Http;

namespace Avatar.Web.Modules.ApiAccess.TeamMemberSkill
{
    public class TeamMemberSkillApiHandler : ITeamMemberSkillApiHandler
    {
        private readonly IFlurlClient flurlClient;

        public TeamMemberSkillApiHandler(IFlurlClientFactory flurlFac, Config.Config config)
        {
            flurlClient = flurlFac.Get(config.ApiUrl);
        }

        public async Task<AppResult<CreateTeamMemberSkillResult>> CreateTeamMemberSkill(CreateTeamMemberSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMemberSkill/CreateTeamMemberSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<CreateTeamMemberSkillResult>();

                return AppResult<CreateTeamMemberSkillResult>.CreateSucceeded(result, "Team member skill created successfully");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<CreateTeamMemberSkillResult>.CreateFailed(ex, $"Failed to create team member skill: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AppResult<CreateTeamMemberSkillResult>.CreateFailed(ex, "An unexpected error occurred while creating team member skill");
            }
        }

        public async Task<AppResult<DeleteTeamMemberSkillResult>> DeleteTeamMemberSkill(DeleteTeamMemberSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMemberSkill/DeleteTeamMemberSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<DeleteTeamMemberSkillResult>();

                return AppResult<DeleteTeamMemberSkillResult>.CreateSucceeded(result, "Team member skill deleted successfully");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<DeleteTeamMemberSkillResult>.CreateFailed(ex, $"Failed to delete team member skill: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AppResult<DeleteTeamMemberSkillResult>.CreateFailed(ex, "An unexpected error occurred while deleting team member skill");
            }
        }

        public async Task<AppResult<GetTeamMemberSkillByIdResult>> GetTeamMemberSkillById(GetTeamMemberSkillByIdArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMemberSkill/GetTeamMemberSkillById")
                    .SetQueryParams(args)
                    .GetJsonAsync<GetTeamMemberSkillByIdResult>();

                return AppResult<GetTeamMemberSkillByIdResult>.CreateSucceeded(result, "Team member skills retrieved successfully");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<GetTeamMemberSkillByIdResult>.CreateFailed(ex, $"Failed to retrieve team member skills: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AppResult<GetTeamMemberSkillByIdResult>.CreateFailed(ex, "An unexpected error occurred while retrieving team member skills");
            }
        }

        public async Task<AppResult<GetAllTeamMemberSkillResult>> GetTeamMemberSkillList()
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMemberSkill/GetAllTeamMemberSkills")
                    .GetJsonAsync<GetAllTeamMemberSkillResult>();

                return AppResult<GetAllTeamMemberSkillResult>.CreateSucceeded(result, "Team member skills retrieved successfully");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<GetAllTeamMemberSkillResult>.CreateFailed(ex, $"Failed to retrieve team member skills: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AppResult<GetAllTeamMemberSkillResult>.CreateFailed(ex, "An unexpected error occurred while retrieving team member skills");
            }
        }

        public async Task<AppResult<UpdateTeamMemberSkillResult>> UpdateTeamMemberSkill(UpdateTeamMemberSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMemberSkill/UpdateTeamMemberSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<UpdateTeamMemberSkillResult>();

                return AppResult<UpdateTeamMemberSkillResult>.CreateSucceeded(result, "Team member skill updated successfully");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<UpdateTeamMemberSkillResult>.CreateFailed(ex, $"Failed to update team member skill: {ex.Message}");
            }
            catch (Exception ex)
            {
                return AppResult<UpdateTeamMemberSkillResult>.CreateFailed(ex, "An unexpected error occurred while updating team member skill");
            }
        }
    }
}