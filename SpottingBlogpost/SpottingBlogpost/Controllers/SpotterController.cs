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

        [HttpPost]
        public IActionResult CreateSpotter([FromBody] UserPostDto userPostDto)
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
    }
}
