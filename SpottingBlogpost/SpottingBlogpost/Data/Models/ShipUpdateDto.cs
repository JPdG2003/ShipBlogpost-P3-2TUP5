using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;
using System.ComponentModel.DataAnnotations;

namespace SpottingBlogpost.Data.Models
{
    public class ShipUpdateDto
    {
        public string Name { get; set; }
        //[RegularExpression(@"^([0-7])$")]
        public ShipType Type { get; set; }
        //[RegularExpression(@"^(1?[0-8]|[0-9])$")]
        public ShipFlag Flag { get; set; }
        //[RegularExpression(@"^([0-3])$")]
        public ShipStatus Status { get; set; }
    }
}


