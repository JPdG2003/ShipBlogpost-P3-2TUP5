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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }


        [HttpPost]
        public IActionResult PostComment(int shipId, [FromBody] CommentDto commentDto)
        {
            var comment = new Comment()
            {
                Content = commentDto.Content,
                CommentType = commentDto.CommentType,
                PosterId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                ShipId = shipId,
            };
            int id = _commentService.PostComment(comment);
            return Ok(id);
        }

        [HttpGet("{shipId}/filteredByShip")]
        public IActionResult GetCommentsByShipId(int shipId)
        {
           var comments = _commentService.GetAllCommentsByShipId(shipId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
                
        }

        [HttpGet("{posterId}/filteredByUser")]
        public IActionResult GetCommentsByPosterId(int posterId)
        {
            var comments = _commentService.GetAllCommentsByPoster(posterId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return NoContent();
        }
    }
}