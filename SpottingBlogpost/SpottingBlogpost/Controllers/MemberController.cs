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
    }
}

