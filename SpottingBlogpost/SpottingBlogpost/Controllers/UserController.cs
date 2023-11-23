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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("Myself")]
        public IActionResult GetMyUser()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var me = _userService.GetUserById(id);

            return Ok(me);
        }


        [HttpPut]
        public IActionResult EditUser([FromBody] UserUpdateDto userUpdateDto)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User userToUpdate = _userService.GetUserById(id);
            {
                userToUpdate.Name = userUpdateDto.Name;
                userToUpdate.LastName = userUpdateDto.LastName;
                userToUpdate.Password = userUpdateDto.Password;
            };
            _userService.UpdateUser(userToUpdate);
            return Ok("User updated");
        }

        [HttpDelete("DeleteMyUser")]
        public IActionResult DeleteMyUser()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role != "Admin")
            {
                User userToDelete = _userService.GetUserForDeletionById(userId);
                _userService.DeleteUser(userToDelete);
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                User userToDelete = _userService.GetUserForDeletionById(userId);
                if (userToDelete != null)
                {
                    _userService.DeleteUser(userToDelete);
                    return NoContent();
                }
                return NotFound();
            }
            return Forbid();
        }

        [HttpPatch("RestoreUser/{userId}")]
        public IActionResult RestoreUser([FromRoute] int userId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                User userToRestore = _userService.GetDeletedUserById(userId);
                if (userToRestore != null)
                {
                    _userService.RestoreUser(userToRestore);
                    return Ok("User restored");
                }
                return NotFound();
            }
            return Forbid();
        }

        [HttpPatch("CascadeRestoreUser/{userId}")]
        public IActionResult CascadeRestoreUser([FromRoute] int userId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                User userToRestore = _userService.GetDeletedUserById(userId);
                if (userToRestore != null)
                {
                    _userService.CascadeRestoreUser(userToRestore);
                    return Ok("User restored");
                }
                return NotFound();
            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedUsers")]
        public IActionResult EraseUsers()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                _userService.EraseUsers();
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedUser/{userId}")]
        public IActionResult EraseDeletedUserById([FromRoute] int userId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                User userToErase = _userService.GetDeletedUserById(userId);
                if (userToErase != null)
                {
                    _userService.EraseUser(userToErase);
                    return NoContent();
                }
                return NotFound();
            }
            return Forbid();
        }
    }
}
