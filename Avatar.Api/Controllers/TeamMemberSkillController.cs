using AutoMapper;
using Avatar.Api.Services.Interfaces;
using Avatar.Framework.ApiCommand;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.ApiCommand.TeamMemberSkill.Request;
using Avatar.Framework.ApiCommand.TeamMemberSkill.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avatar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberSkillController : ControllerBase
    {
        private readonly ITeamMemberSkillRepository teamMemberSkillRepository;
        private readonly IMapper mapper;
        public TeamMemberSkillController(ITeamMemberSkillRepository teamMemberSkillRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.teamMemberSkillRepository = teamMemberSkillRepository;
        }
        [Route("GetAllTeamMemberSkills")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetAllTeamMemberSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeamMemberSkills([FromQuery] GetAllTeamMemberSkillArgs args)
        {
            try
            {
                var result = await teamMemberSkillRepository.GetAllAsync();
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new GetAllTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new GetAllTeamMemberSkillResult { Result = result.Result, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new GetAllTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }

        [Route("GetTeamMemberSkillById")]
        [HttpGet]
        [ProducesResponseType(typeof(GetTeamMemberSkillByIdResult), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetTeamMemberSkillById([FromQuery] GetTeamMemberSkillByIdArgs args)
        {
            try
            {
                var result = await teamMemberSkillRepository.GetTeamMemberSkillById(args.SkillId);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new GetTeamMemberSkillByIdResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new GetTeamMemberSkillByIdResult { Result = result.Result, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new GetTeamMemberSkillByIdResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }

        [Route("CreateTeamMemberSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateTeamMemberSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTeamMemberSkill([FromBody] CreateTeamMemberSkillArgs args)
        {
            try
            {
                var dtoTeamMemberSkill = mapper.Map<TeamMemberSkillDTO>(args);
                var result = await teamMemberSkillRepository.CreateTeamMemberSkill(dtoTeamMemberSkill);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new CreateTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new CreateTeamMemberSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new CreateTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("UpdateTeamMemberSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UpdateTeamMemberSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeamMemberSkill([FromBody] UpdateTeamMemberSkillArgs args)
        {
            try
            {
                var dtoTeamMemberSkill = mapper.Map<TeamMemberSkillDTO>(args);
                var result = await teamMemberSkillRepository.UpdateTeamMemberSkill(dtoTeamMemberSkill);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new UpdateTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new UpdateTeamMemberSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new UpdateTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("DeleteTeamMemberSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DeleteTeamMemberSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTeamMemberSkill([FromBody] DeleteTeamMemberSkillArgs args)
        {
            try
            {
                var result = await teamMemberSkillRepository.DeleteTeamMemberSkill(args.Id);
                if (!result.Succeeded || !result.Result)
                {
                    return new JsonResult(new DeleteTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new DeleteTeamMemberSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new DeleteTeamMemberSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
    }
}
