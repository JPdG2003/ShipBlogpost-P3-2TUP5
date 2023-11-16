﻿using Microsoft.AspNetCore.Authorization;
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
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteSpotter()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value); 
            _userService.DeleteUser(userId);
            return NoContent();
        }
    }
}
