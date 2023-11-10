using SpottingBlogpost.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SpottingBlogpost.Data.Enum.Ship;

namespace SpottingBlogpost.Data.Models
{
    public class ShipDto
    {
        public string Name { get; set; }
        public ShipType Type { get; set; }
        public ShipFlag Flag { get; set; }
    }
}
