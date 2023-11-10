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
        private readonly IUserService _userService;
        public ShipController (IShipService shipService, IUserService userService)
        {
            _shipService = shipService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllShips() 
        {
            return Ok(_shipService.GetAllShips());
        }

        [HttpGet("{shipFlag}/filteredByFlag")]
        [Authorize]
        public IActionResult GetShipsByFlag(ShipFlag shipFlag)
        {
            var ships = _shipService.GetAllShipsByFlag(shipFlag);
            if (ships == null)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpGet("{shipType}/filteredByType")]
        [Authorize]
        public IActionResult GetShipsByType(ShipType shipType)
        {
            var ships = _shipService.GetAllShipsByType(shipType);
            if (ships == null)
            {
                return NotFound();
            }
            return Ok(ships);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddShip([FromBody] ShipDto shipDto)
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

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateShip([FromRoute] int id, [FromBody] ShipUpdateDto shipUpdateDto) 
        {
            var ship = new Ship()
            {
                Name = shipUpdateDto.Name,
                Type = shipUpdateDto.Type,
                Flag = shipUpdateDto.Flag,
                Status = shipUpdateDto.Status,
                SpotterId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
            };
            _shipService.UpdateShip(ship);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteShip(int id)
        {
            Ship shipToDelete = _shipService.GetShipById(id);
            if (shipToDelete == null)
            {
                return NotFound();
            }
            _shipService.DeleteShip(shipToDelete);

            return NoContent();
        }
    }
}

