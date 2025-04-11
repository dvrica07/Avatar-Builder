using System.ComponentModel.DataAnnotations;

namespace Avatar.Web.Models;
public class SkillModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
}
