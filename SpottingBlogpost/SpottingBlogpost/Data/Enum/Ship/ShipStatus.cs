using System.ComponentModel;

namespace SpottingBlogpost.Data.Enum.Ship
{
    public enum ShipStatus
    {
        [Description("The ship is soon to arrive to port")]
        Arriving,
        [Description("The ship has reached the port")]
        InPort,
        [Description("The ship has berthed at the port")]
        Berthed,
        [Description("The ship is soon to leave the port")]
        Departing
    }
}
