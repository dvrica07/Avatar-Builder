using AutoMapper;
using Avatar.Api.Repository.Entities;
using Avatar.Framework.ApiCommand.DTO;
using Avatar.Framework.ApiCommand.Skill.Request;
using Avatar.Framework.ApiCommand.TeamMember.Request;
using Avatar.Framework.ApiCommand.TeamMemberSkill.Request;


namespace Avatar.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TeamMember, TeamMemberDTO>();
        CreateMap<TeamMemberDTO, TeamMember>();
        CreateMap<CreateTeamMemberArgs, TeamMemberDTO>();
        CreateMap<UpdateTeamMembeArgs, TeamMemberDTO>();


        CreateMap<TeamMemberSkill, TeamMemberSkillDTO>();
        CreateMap<TeamMemberSkillDTO, TeamMemberSkill>();
        CreateMap<CreateTeamMemberSkillArgs, TeamMemberSkillDTO>();
        CreateMap<UpdateTeamMemberSkillArgs, TeamMemberSkillDTO>();

        CreateMap<Skill, SkillDTO>();   
        CreateMap<SkillDTO, Skill>();
        CreateMap<CreateSkillArgs, SkillDTO>(); 
        CreateMap<UpdateSkillArgs, SkillDTO>();

    }
}
