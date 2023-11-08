namespace SpottingBlogpost.Data.Entities
{
    public class Spotter : User
    {
        public ICollection<Ship> SpottedShips { get; set; } = new List<Ship>();
    }
}
