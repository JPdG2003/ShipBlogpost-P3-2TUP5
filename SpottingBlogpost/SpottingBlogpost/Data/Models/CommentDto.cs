using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpottingBlogpost.Data.Models
{
    public class CommentDto
    {
        public string Content { get; set; }
        public Ship CommentedShip { get; set; }
        public int ShipId { get; set; }
        public User Poster { get; set; }
        public int PosterId { get; set; }
        public CommentType CommentType { get; set; }
    }
}
