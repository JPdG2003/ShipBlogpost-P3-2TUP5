using Microsoft.AspNetCore.Mvc;
using SpottingBlogpost.Data;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;
using SpottingBlogpost.Services.Interfaces;

namespace SpottingBlogpost.Services.Implementations
{
    public class ShipService : IShipService
    {
        private readonly SpottingContext _context;

        public ShipService(SpottingContext context)
        {
            _context = context;
        }

        public List<Ship> GetAllShips()
        {
            return _context.Ships.ToList();
        }

        public Ship? GetShipById(int shipId)
        {
            return _context.Ships.SingleOrDefault(s => s.Id == shipId);
        }

        public int AddShip(Ship ship)
        {
            _context.Add(ship);
            _context.SaveChanges();

            return ship.Id;
        }

        public void UpdateShip(Ship shipToUpdate)
        {
            _context.Update(shipToUpdate);
            _context.SaveChanges();
        }

        public void DeleteShip(Ship shipToDelete)
        {
            shipToDelete.IsDeleted = true;
            _context.Update(shipToDelete);
            _context.SaveChanges();
        }

        public List<Ship> GetAllShipsByStatus (ShipStatus shipStatus)
        {
            return _context.Ships
                .Where(s => s.Status == shipStatus)
                .ToList();
        }

        public List<Ship> GetAllShipsByType (ShipType shipType)
        {
            return _context.Ships
                .Where(t => t.Type == shipType)
                .ToList();

        }

        public List<Ship> GetAllShipsByFlag (ShipFlag shipFlag)
        {
            return _context.Ships
                .Where (f => f.Flag == shipFlag)
                .ToList();
        }

        public List<Ship> GetAllShipsBySpotterId (int spotterId)
        {
            return _context.Ships
                .Where(s => s.SpotterId == spotterId)
                .ToList();
        }
    }
}
