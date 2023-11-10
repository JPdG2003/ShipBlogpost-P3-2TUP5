using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface IShipService
    {
        public List<Ship> GetAllShips();
        public Ship? GetShipById(int shipId);
        public int AddShip(Ship ship);
        public void UpdateShip(Ship ship);
        public void DeleteShip(Ship shipToDelete);
        public List<Ship> GetAllShipsByStatus(ShipStatus shipStatus);
        public List<Ship> GetAllShipsByType(ShipType shipType);
        public List<Ship> GetAllShipsByFlag(ShipFlag shipFlag);
        public List<Ship> GetAllShipsBySpotterId(int spotterId);
    }
}
