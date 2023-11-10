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
    public class MemberController : ControllerBase
    {
        private readonly IUserService _userService;
        public MemberController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateMember([FromBody] UserPostDto userPostDto)
        {
            var member = new Member()
            {
                Name = userPostDto.Name,
                LastName = userPostDto.LastName,
                Email = userPostDto.Email,
                Password = userPostDto.Password,
                Username = userPostDto.Username,
                UserType = "Member"
            };
            int id = _userService.CreateUser(member);
            return Ok(id);
        }

        [HttpPut]
        [Authorize]
        public IActionResult EditMember([FromBody] UserUpdateDto userUpdateDto)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (role == "Member") 
            {
                Member memberToUpdate = new Member()
                {
                    Id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                    Username = User.Claims.FirstOrDefault(c => c.Type.Contains("username")).Value,
                    Name = userUpdateDto.Name,
                    LastName= userUpdateDto.LastName,
                    Password = userUpdateDto.Password,
                    UserType = "Member",
                };
                _userService.UpdateUser(memberToUpdate);
                return Ok();
            }
            return Forbid();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult DeleteMember()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            _userService.DeleteUser(id);
            return NoContent();
        }

    }
}
