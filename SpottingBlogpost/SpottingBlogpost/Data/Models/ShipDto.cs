using SpottingBlogpost.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SpottingBlogpost.Data.Enum.Ship;

namespace SpottingBlogpost.Data.Models
{
    public class ShipDto
    {
        public string Name { get; set; }
        //[RegularExpression(@"^([0-7])$")]
        public ShipType Type { get; set; }
        //[RegularExpression(@"^(1?[0-8]|[0-9])$")]
        public ShipFlag Flag { get; set; }
    }
}
