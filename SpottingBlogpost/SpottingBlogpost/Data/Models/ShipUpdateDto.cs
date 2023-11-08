using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;

namespace SpottingBlogpost.Data.Models
{
    public class ShipUpdateDto
    {
        public string Name { get; set; }
        public ShipType Type { get; set; }  
        public ShipFlag Flag { get; set; }
        public ShipStatus Status { get; set; }
    }
}

