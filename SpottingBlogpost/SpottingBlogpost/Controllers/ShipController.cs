using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum.Ship;
using SpottingBlogpost.Data.Models;
using SpottingBlogpost.Services.Interfaces;
using System.Security.Claims;

namespace SpottingBlogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;
        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public IActionResult GetAllShips()
        {
            return Ok(_shipService.GetAllShips());
        }

        #region ID Filters

        [HttpGet("Ships/{shipId}")]
        [Authorize]
        public IActionResult GetShipById(int shipId)
        {
            var ship = _shipService.GetShipById(shipId);
            if (ship == null)
            {
                return NotFound();
            }
            return Ok(ship);
        }

        [HttpGet("Spotter/{spotterId}")]
        [Authorize]
        public IActionResult GetShipsBySpotterId(int spotterId)
        {
            var ships = _shipService.GetAllShipsBySpotterId(spotterId);
            if (ships.Count == 0)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        #endregion

        #region Enum Filters

        [HttpGet("Flag/{shipFlag}")]
        [Authorize]
        public IActionResult GetShipsByFlag([FromRoute] ShipFlag shipFlag)
        {
            var ships = _shipService.GetAllShipsByFlag(shipFlag);
            if (ships.Count == 0)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpGet("Type/{shipType}")]
        [Authorize]
        public IActionResult GetShipsByType([FromRoute] ShipType shipType)
        {
            var ships = _shipService.GetAllShipsByType(shipType);
            if (ships.Count == 0)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpGet("Status/{shipStatus}")]
        [Authorize]
        public IActionResult GetShipsByStatus([FromRoute] ShipStatus shipStatus)
        {
            var ships = _shipService.GetAllShipsByStatus(shipStatus);
            if (ships.Count == 0)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        #endregion

        [HttpPost]
        [Authorize]
        public IActionResult AddShip([FromBody] ShipDto shipDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Spotter" || role == "Admin")
            {
                var ship = new Ship()
                {
                    Name = shipDto.Name,
                    Type = shipDto.Type,
                    Flag = shipDto.Flag,
                    SpotterId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                };
                int id = _shipService.AddShip(ship);
                return Ok(id);
            }
            return Forbid();

        }

        [HttpPut("{shipId}")]
        [Authorize]
        public IActionResult UpdateShip([FromRoute] int shipId, [FromBody] ShipUpdateDto shipUpdateDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter" || role == "Admin")
            {
                Ship shipToUpdate = _shipService.GetShipById(shipId);
                if (shipToUpdate == null)
                {
                    return NotFound();
                }
                shipToUpdate.Name = shipUpdateDto.Name;
                shipToUpdate.Type = shipUpdateDto.Type;
                shipToUpdate.Flag = shipUpdateDto.Flag;
                shipToUpdate.Status = shipUpdateDto.Status;

                _shipService.UpdateShip(shipToUpdate);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete("{shipId}")]
        [Authorize]
        public IActionResult DeleteShip([FromRoute] int shipId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter" || role == "Admin")
            {
                Ship shipToDelete = _shipService.GetShipById(shipId);
                if (shipToDelete == null)
                {
                    return NotFound();
                }
                _shipService.DeleteShip(shipToDelete);

                return NoContent();
            }
            return Forbid();
        }

        [HttpPatch("{shipId}")]
        [Authorize]
        public IActionResult RestoreShip([FromRoute] int shipId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter" || role == "Admin")
            {
                Ship shipToRestore = _shipService.GetDeletedShipById(shipId);
                if (shipToRestore == null)
                {
                    return NotFound();
                }
                _shipService.RestoreShip(shipToRestore);

                return Ok("Ship restored");

            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedShips")]
        [Authorize]
        public IActionResult EraseDeletedShips()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                _shipService.EraseShips();
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedShipById/{shipId}")]
        [Authorize]
        public IActionResult EraseDeletedShipById([FromRoute] int shipId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Ship shipToErase = _shipService.GetDeletedShipById(shipId);
                if (shipToErase == null)
                {
                    return NotFound();
                }
                _shipService.EraseShip(shipToErase);
                return NoContent();
            }
            return Forbid();
        }
    }
}

