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
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] UserPostDto userPostDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                var admin = new Admin()
                {
                    Name = userPostDto.Name,
                    LastName = userPostDto.LastName,
                    Email = userPostDto.Email,
                    Password = userPostDto.Password,
                    Username = userPostDto.Username,
                    UserType = "Admin"
                };
                int id = _userService.CreateUser(admin);
                return Ok(id);
            }
            return Forbid();
        }
    }
}

