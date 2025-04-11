using AutoMapper;
using Avatar.Api.Services.Interfaces;
using Avatar.Framework.ApiCommand;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.ApiCommand.TeamMember.Request;
using Avatar.Framework.ApiCommand.TeamMember.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avatar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository teamMemberRepository;
        private readonly IMapper mapper;
        public TeamMemberController(ITeamMemberRepository teamMemberRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.teamMemberRepository = teamMemberRepository;
        }
        [Route("GetAllTeamMembers")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetAllTeamMemberResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeamMembers([FromQuery] GetAllTeamMemberArgs args)
        {
            try
            {
                var result = await teamMemberRepository.GetAllAsync();
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new GetAllTeamMemberResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new GetAllTeamMemberResult { Result = result.Result, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new GetAllTeamMemberResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("CreateTeamMember")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateTeamMemberResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTeamMember([FromBody] CreateTeamMemberArgs args)
        {
            try
            {
                var dtoTeamMember = mapper.Map<TeamMemberDTO>(args);
                var result = await teamMemberRepository.CreateTeamMember(dtoTeamMember);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new CreateTeamMemberResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new CreateTeamMemberResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new CreateTeamMemberResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("UpdateTeamMember")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UpdateTeamMemberResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeamMember([FromBody] UpdateTeamMembeArgs args)
        {
            try
            {
                var dtoTeamMember = mapper.Map<TeamMemberDTO>(args);
                var result = await teamMemberRepository.UpdateTeamMember(dtoTeamMember);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new UpdateTeamMemberResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new UpdateTeamMemberResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new UpdateTeamMemberResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("DeleteTeamMember")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DeleteTeamMemberResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTeamMember([FromBody] DeleteTeamMemberArgs args)
        {
            try
            {
                var result = await teamMemberRepository.DeleteTeamMember(args.Id);
                if (!result.Succeeded || !result.Result)
                {
                    return new JsonResult(new DeleteTeamMemberResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new DeleteTeamMemberResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new DeleteTeamMemberResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }

    }
}
