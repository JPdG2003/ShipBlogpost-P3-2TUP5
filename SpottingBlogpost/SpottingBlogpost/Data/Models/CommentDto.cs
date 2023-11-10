using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpottingBlogpost.Data.Models
{
    public class CommentDto
    {
        public string Content { get; set; }
        public CommentType CommentType { get; set; }
    }
}
