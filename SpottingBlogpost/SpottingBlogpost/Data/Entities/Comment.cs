using SpottingBlogpost.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpottingBlogpost.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; } = DateTime.Now.ToUniversalTime();


        [ForeignKey("ShipId")]
        public Ship CommentedShip { get; set; }
        public int ShipId { get; set;}


        [ForeignKey("PosterId")]
        public User Poster { get; set; }  
        public int PosterId { get; set;}

        public bool IsDeleted { get; set; } = false;

        [Required]
        public CommentType CommentType { get; set;}
    }
}
