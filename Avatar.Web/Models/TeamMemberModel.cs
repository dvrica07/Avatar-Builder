﻿using System.ComponentModel.DataAnnotations;

namespace Avatar.Web.Models
{
    public class TeamMemberModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}
