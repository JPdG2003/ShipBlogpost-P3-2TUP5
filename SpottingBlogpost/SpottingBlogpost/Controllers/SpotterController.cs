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

        [HttpPut]
        public IActionResult EditSpotter([FromBody] UserUpdateDto userUpdateDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (role == "Spotter")
            {
                User userToUpdate = _userService.GetUserById(id);
                {
                    userToUpdate.Name = userUpdateDto.Name;
                    userToUpdate.LastName = userUpdateDto.LastName;
                    userToUpdate.Password = userUpdateDto.Password;
                };
                _userService.UpdateUser(userToUpdate);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete]
        public IActionResult DeleteSpotter()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);;
            _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
