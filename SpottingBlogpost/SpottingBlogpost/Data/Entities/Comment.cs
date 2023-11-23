using SpottingBlogpost.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpottingBlogpost.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CommentDate { get; } = DateTime.Now.ToUniversalTime();

        [ForeignKey("ShipId")]
        public Ship CommentedShip { get; set; }
        [Required]
        public int ShipId { get; set;}


        [ForeignKey("PosterId")]
        public User Poster { get; set; }  
        public int PosterId { get; set;}

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; }

        [Required]
        public CommentType CommentType { get; set;}
    }
}
