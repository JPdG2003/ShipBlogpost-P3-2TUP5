﻿using System.ComponentModel.DataAnnotations;

namespace SpottingBlogpost.Data.Models
{
    public class UserPostDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
