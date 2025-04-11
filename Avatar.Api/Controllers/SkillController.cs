using AutoMapper;
using Avatar.Api.Services.Interfaces;
using Avatar.Framework.ApiCommand;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.ApiCommand.Skill.Request;
using Avatar.Framework.ApiCommand.Skill.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avatar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class SkillController : ControllerBase
    {
        private readonly ISkillRepository skillRepository;
        private readonly IMapper mapper;
        public SkillController(ISkillRepository skillRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.skillRepository = skillRepository;
        }
        [Route("GetAllSkills")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GetAllSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSkills([FromQuery] GetAllSkillArgs arsg)
        {
            try
            {
                var result = await skillRepository.GetAllAsync();
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new GetAllSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new GetAllSkillResult { Result = result.Result, IsSuccess = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new GetAllSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("CreateSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreateSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateSkill([FromBody] CreateSkillArgs args)
        {
            try
            {
                var dtoSkill = mapper.Map<SkillDTO>(args);
                var result = await skillRepository.CreateSkill(dtoSkill);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new CreateSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new CreateSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new CreateSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("UpdateSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UpdateSkillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSkill([FromBody] UpdateSkillArgs args)
        {
            try
            {
                var dtoSkill = mapper.Map<SkillDTO>(args);
                var result = await skillRepository.UpdateSkill(dtoSkill);
                if (!result.Succeeded || result.Result is null)
                {
                    return new JsonResult(new UpdateSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }
                return new JsonResult(new UpdateSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new UpdateSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
        [Route("DeleteSkill")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(DeleteSkillResult), StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteSkill([FromBody] DeleteSkillArgs args)
        {
            try
            {
                var result = await skillRepository.DeleteSkill(args.Id);
                if (!result.Succeeded || !result.Result)
                {
                    return new JsonResult(new DeleteSkillResult { ErrorInfo = new ErrorInfo { Message = result.Message } });
                }

                return new JsonResult(new DeleteSkillResult { IsSuccess = true, Result = result.Result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new DeleteSkillResult { ErrorInfo = new ErrorInfo { Message = ex.Message } });
            }
        }
    }
}
