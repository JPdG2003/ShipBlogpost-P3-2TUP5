using SpottingBlogpost.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SpottingBlogpost.Data.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }

    }
}
