using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Models;
using SpottingBlogpost.Services.Interfaces;
using System.Security.Claims;

namespace SpottingBlogpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SpotterController : ControllerBase
    {
        private readonly IUserService _userService;
        public SpotterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetSpotters")]
        public IActionResult GetAllSpotters()
        {
            string userType = "Spotter";
            return Ok(_userService.GetUsersByType(userType));
        }

        [HttpPost]
        public IActionResult CreateSpotter([FromBody] UserPostDto userPostDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                var spotter = new Spotter()
                {
                    Name = userPostDto.Name,
                    LastName = userPostDto.LastName,
                    Email = userPostDto.Email,
                    Password = userPostDto.Password,
                    Username = userPostDto.Username,
                    UserType = "Spotter"
                };
                int id = _userService.CreateUser(spotter);
                return Ok(id);
            }
            return Forbid();
        }
    }
}
