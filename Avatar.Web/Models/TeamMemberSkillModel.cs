using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Avatar.Framework.Enums.Enums;

namespace Avatar.Web.Models
{
    public class TeamMemberSkillModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team member is required")]
        [ForeignKey(nameof(TeamMember))]
        [Range(1, int.MaxValue, ErrorMessage = "Team member must be selected")]
        public int TeamMemberId { get; set; }

        [Required(ErrorMessage = "Skill is required")]
        [ForeignKey(nameof(Skill))]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a skill")]
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Proficiency level is required")]
        [Column(TypeName = "nvarchar(20)")]
        public ProficiencyLevel Level { get; set; }

        public TeamMemberModel TeamMember { get; set; }
        public SkillModel Skill { get; set; }

    }
}
