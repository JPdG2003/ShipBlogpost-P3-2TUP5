using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum;
using SpottingBlogpost.Data.Models;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface ICommentService
    {
        public Comment? GetCommentById(int id);
        public List<Comment> GetAllCommentsByShipId(int shipId);
        public List<Comment> GetAllCommentsByPoster(int posterId);
        public List<Comment> GetAllCommentsPerShipByType(int shipId, CommentType commentType);
        public int PostComment(Comment comment);
        public void DeleteComment(Comment commentToDelete);
        public Comment? GetDeletedCommentById(int commentId);
        public void RestoreComment(Comment commentToRestore);
        public void EraseComments();
        public void EraseComment(Comment commentToErase);
        public bool ValidateEnum(CommentDto commentDto);

    }
}
