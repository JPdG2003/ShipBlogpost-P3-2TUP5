using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ShipController (IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public IActionResult GetAllShips() 
        {
            return Ok(_shipService.GetAllShips());
        }

        #region ID Filters

        [HttpGet("ships/{shipId}")]
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

        [HttpGet("spotter/{spotterId}")]
        [Authorize]
        public IActionResult GetShipsBySpotterId(int spotterId)
        {
            var ships = _shipService.GetAllShipsBySpotterId(spotterId);
            if (ships == null)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        #endregion

        #region Enum Filters

        [HttpGet("flag/{shipFlag}")]
        [Authorize]
        public IActionResult GetShipsByFlag([FromRoute] ShipFlag shipFlag)
        {
            var ships = _shipService.GetAllShipsByFlag(shipFlag);
            if (ships == null)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpGet("type/{shipType}")]
        [Authorize]
        public IActionResult GetShipsByType([FromRoute] ShipType shipType)
        {
            var ships = _shipService.GetAllShipsByType(shipType);
            if (ships == null)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpGet("status/{shipStatus}")]
        [Authorize]
        public IActionResult GetShipsByStatus([FromRoute] ShipStatus shipStatus)
        {
            var ships = _shipService.GetAllShipsByStatus(shipStatus);
            if (ships == null)
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
            
            if (role == "Spotter") 
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
            if (role == "Spotter") 
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
            if (role == "Spotter")
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
    }
}

