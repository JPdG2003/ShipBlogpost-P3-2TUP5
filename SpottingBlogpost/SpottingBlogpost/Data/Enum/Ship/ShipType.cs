using System.ComponentModel;

namespace SpottingBlogpost.Data.Enum.Ship
{
    public enum ShipType
    {
        ContainerShip,
        BulkCarrier,
        TankerShip,
        PassengerShip,
        NavalShip,
        OffshoreShip,
        SpecialPurpose,
        [Description("All ships of strictly military use fit here")]
        MilitaryVarious
    }
}
