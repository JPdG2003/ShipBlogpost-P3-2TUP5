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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpPost("{shipId}")]
        public IActionResult PostComment([FromRoute] int shipId, [FromBody] CommentDto commentDto)
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

        [HttpGet("shipComments/{shipId}")]
        public IActionResult GetCommentsByShipId([FromRoute] int shipId)
        {
            var comments = _commentService.GetAllCommentsByShipId(shipId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);

        }

        [HttpGet("userComments/{posterId}")]
        public IActionResult GetCommentsByPosterId([FromRoute] int posterId)
        {
            var comments = _commentService.GetAllCommentsByPoster(posterId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpDelete("{commentId}")]
        public IActionResult DeleteComment([FromRoute] int commentId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter")
            {
                var commentToDelete = _commentService.GetCommentById(commentId);
                if (commentToDelete == null)
                {
                    return NotFound();
                }
                _commentService.DeleteComment(commentToDelete);
                return NoContent();
            }
            return Forbid();
        }
    }
}
