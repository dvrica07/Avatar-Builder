using Avatar.Framework.ApiCommand.Skill.Request;
using Avatar.Framework.ApiCommand.Skill.Response;
using Avatar.Framework.Common;
using Avatar.Web.Modules.ApiAccess.Handlers;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace Avatar.Web.Modules.ApiAccess.Skill
{
    public class SkillApiHandler : ISkillApiHandler
    {
        private readonly IFlurlClient flurlClient;
        public SkillApiHandler(IFlurlClientFactory flurlFac, Config.Config config)
        {
            flurlClient = flurlFac.Get(config.ApiUrl);
        }

        public async Task<AppResult<CreateSkillResult>> CreateSkill(CreateSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("Skill/CreateSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<CreateSkillResult>();
                return AppResult<CreateSkillResult>.CreateSucceeded(result, "Successfully created skill");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<CreateSkillResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<CreateSkillResult>.CreateFailed(ex, "An error occured when posting create api");
            }
        }

        public async Task<AppResult<DeleteSkillResult>> DeleteSkill(DeleteSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("Skill/DeleteSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<DeleteSkillResult>();
                return AppResult<DeleteSkillResult>.CreateSucceeded(result, "Successfully deleted skill");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<DeleteSkillResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<DeleteSkillResult>.CreateFailed(ex, "An error occurred when deleting skill");
            }
        }

        public async Task<AppResult<GetAllSkillResult>> GetSkillList()
        {
            try
            {
                var result = await flurlClient
                    .Request("Skill/GetAllSkills")
                    .GetJsonAsync<GetAllSkillResult>();
                return AppResult<GetAllSkillResult>.CreateSucceeded(result, "Skills successfully retrieved");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<GetAllSkillResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<GetAllSkillResult>.CreateFailed(ex, "An error occurred when retrieving skill list");
            }
        }

        public async Task<AppResult<UpdateSkillResult>> UpdateSkill(UpdateSkillArgs args)
        {
            try
            {
                var result = await flurlClient
                    .Request("Skill/UpdateSkill")
                    .PostJsonAsync(args)
                    .ReceiveJson<UpdateSkillResult>();
                return AppResult<UpdateSkillResult>.CreateSucceeded(result, "Successfully updated skill");
            }
            catch (FlurlHttpException ex)
            {
                return AppResult<UpdateSkillResult>.CreateFailed(ex, ex.Message);
            }
            catch (Exception ex)
            {
                return AppResult<UpdateSkillResult>.CreateFailed(ex, "An error occurred when updating skill");
            }
        }
    }
}
