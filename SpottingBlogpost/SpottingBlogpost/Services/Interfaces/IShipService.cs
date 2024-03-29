﻿using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;
using SpottingBlogpost.Data.Models;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface IShipService
    {
        public List<Ship> GetAllShips();

        public Ship? GetShipById(int shipId);
        public int AddShip(Ship ship);
        public void UpdateShip(Ship shipToUpdate);
        public void DeleteShip(Ship shipToDelete);
        public List<Ship> GetAllShipsByStatus(ShipStatus shipStatus);
        public List<Ship> GetAllShipsByType(ShipType shipType);
        public List<Ship> GetAllShipsByFlag(ShipFlag shipFlag);
        public List<Ship> GetAllShipsBySpotterId(int spotterId);
        public Ship? GetDeletedShipById(int shipId);
        public void RestoreShip(Ship shipToRestore);
        public void CascadeRestoreShip(Ship shipToRestore);
        public void EraseShips();
        public void EraseShip(Ship shipToErase);
        public bool ValidatePostEnum(ShipDto shipDto);
        public bool ValidateUpdateEnum(ShipUpdateDto shipUpdateDto);
    }
}
