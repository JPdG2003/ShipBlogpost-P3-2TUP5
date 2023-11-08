using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpottingBlogpost.Data.Enum.Ship;

namespace SpottingBlogpost.Data.Entities
{
    public class Ship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ShipType Type { get; set; }
        public ShipFlag Flag { get; set; }
        public ShipStatus Status { get; set; } = ShipStatus.Arriving;
        public DateTime SpottingDate { get; } = DateTime.Now.ToUniversalTime();

        [ForeignKey("SpotterId")]
        public Spotter Spotter { get; set; }
        public int SpotterId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

