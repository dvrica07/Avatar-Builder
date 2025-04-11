using Flurl.Http.Configuration;
using Flurl.Http;
using Avatar.Framework.ApiCommand.TeamMember.Request;
using Avatar.Framework.ApiCommand.TeamMember.Response;
using Avatar.Framework.Common;
using Avatar.Web.Modules.ApiAccess.Handlers;

namespace Avatar.Web.Modules.ApiAccess.TeamMember
{
    public class TeamMemberApihandler : ITeamMemberApiHandler
    {
        private readonly IFlurlClient flurlClient;
        public TeamMemberApihandler(IFlurlClientFactory flurlFac, Config.Config config)
        {
            flurlClient = flurlFac.Get(config.ApiUrl);
        }
        public async Task<AppResult<GetAllTeamMemberResult>> GetTeamMemberList()
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMember/GetAllTeamMembers")
                    .GetJsonAsync<GetAllTeamMemberResult>();
                return AppResult<GetAllTeamMemberResult>.CreateSucceeded(result, "Successfully retrieved team member list");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<GetAllTeamMemberResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<GetAllTeamMemberResult>.CreateFailed(ex, "An error occurred when retrieving team member list");
            }
        }

        public async Task<AppResult<CreateTeamMemberResult>> CreateTeamMember(CreateTeamMemberArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMember/CreateTeamMember")
                    .PostJsonAsync(args)
                    .ReceiveJson<CreateTeamMemberResult>();
                return AppResult<CreateTeamMemberResult>.CreateSucceeded(result, "Successfully created team member");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<CreateTeamMemberResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<CreateTeamMemberResult>.CreateFailed(ex, "An error occurred when creating team member");
            }
        }

        public async Task<AppResult<DeleteTeamMemberResult>> DeleteTeamMember(DeleteTeamMemberArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMember/DeleteTeamMember")
                    .PostJsonAsync(args)
                    .ReceiveJson<DeleteTeamMemberResult>();
                return AppResult<DeleteTeamMemberResult>.CreateSucceeded(result, "Successfully deleted team member");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<DeleteTeamMemberResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<DeleteTeamMemberResult>.CreateFailed(ex, "An error occurred when deleting team member");
            }
        }

        public async Task<AppResult<UpdateTeamMemberResult>> UpdateTeamMember(UpdateTeamMembeArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("TeamMember/UpdateTeamMember")
                    .PostJsonAsync(args)
                    .ReceiveJson<UpdateTeamMemberResult>();
                return AppResult<UpdateTeamMemberResult>.CreateSucceeded(result, "Successfully updated team member");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<UpdateTeamMemberResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<UpdateTeamMemberResult>.CreateFailed(ex, "An error occurred when updating team member");
            }
        }
    }
}
