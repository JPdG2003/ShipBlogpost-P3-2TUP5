using System.ComponentModel.DataAnnotations;

namespace SpottingBlogpost.Data.Models
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
