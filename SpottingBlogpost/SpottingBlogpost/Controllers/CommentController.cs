using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum;
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
        private readonly IShipService _shipService;
        public CommentController(ICommentService commentService, IShipService shipService)
        {
            _commentService = commentService;
            _shipService = shipService;
        }


        [HttpPost("NewComment/{shipId}")]
        public IActionResult PostComment([FromRoute] int shipId, [FromBody] CommentDto commentDto)
        {
            bool validateEnum = _commentService.ValidateEnum(commentDto);
            if (validateEnum)
            {
                Ship shipToComment = _shipService.GetShipById(shipId);
                if (shipToComment != null)
                {
                    var comment = new Comment()
                    {
                        Content = commentDto.Content,
                        CommentType = commentDto.CommentType,
                        PosterId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
                        ShipId = shipToComment.Id,
                    };
                    int id = _commentService.PostComment(comment);
                    return Ok(id);
                }
                return NotFound();
            }
            return BadRequest("Parameter CommentType is invalid");
        }

        [HttpGet("ShipComments/{shipId}")]
        public IActionResult GetCommentsByShipId([FromRoute] int shipId)
        {
            var comments = _commentService.GetAllCommentsByShipId(shipId);
            if (comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("ShipComments/{shipId}/{commentType}")]
        public IActionResult GetCommentsPerShipByType([FromRoute] int shipId, CommentType commentType)
        {
            var comments = _commentService.GetAllCommentsPerShipByType(shipId, commentType);
            if (comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("UserComments/{posterId}")]
        public IActionResult GetCommentsByPosterId([FromRoute] int posterId)
        {
            var comments = _commentService.GetAllCommentsByPoster(posterId);
            if (comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpDelete("DeleteComment/{commentId}")]
        public IActionResult DeleteComment([FromRoute] int commentId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter" || role == "Admin")
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

        [HttpPatch("RestoreComment/{commentId}")]
        public IActionResult RestoreComment([FromRoute] int commentId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Spotter" || role == "Admin")
            {
                Comment commentToRestore = _commentService.GetDeletedCommentById(commentId);
                if (commentToRestore == null)
                {
                    return NotFound();
                }
                _commentService.RestoreComment(commentToRestore);
                return Ok("Comment restored");
            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedComments")]
        public IActionResult EraseDeletedComments()
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                _commentService.EraseComments();
                return NoContent();
            }
            return Forbid();
        }

        [HttpDelete("EraseDeletedCommentById/{commentId}")]
        public IActionResult EraseDeletedCommentById([FromRoute] int commentId)
        {
            string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (role == "Admin")
            {
                Comment commentToErase = _commentService.GetDeletedCommentById(commentId);
                if (commentToErase == null)
                {
                    return NotFound();
                }
                _commentService.EraseComment(commentToErase);
                return NoContent();
            }
            return Forbid();
        }
    }
}
