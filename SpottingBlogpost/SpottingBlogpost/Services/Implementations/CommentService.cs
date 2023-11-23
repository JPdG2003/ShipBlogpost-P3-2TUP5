using SpottingBlogpost.Data;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Enum;
using SpottingBlogpost.Services.Interfaces;

namespace SpottingBlogpost.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly SpottingContext _context;

        public CommentService(SpottingContext context)
        {
            _context = context;
        }

        public Comment? GetCommentById(int id)
        {
            return _context.Comments.SingleOrDefault(c => c.Id == id && !c.IsDeleted);
        }

        public List<Comment> GetAllCommentsByShipId(int shipId)
        {
            return _context.Comments
                .Where(s => s.ShipId == shipId && !s.IsDeleted)
                .ToList();
        }

        public List<Comment> GetAllCommentsByPoster(int posterId)
        {
            return _context.Comments
                .Where(p => p.PosterId == posterId && !p.IsDeleted)
                .ToList();
        }

        public List<Comment> GetAllCommentsPerShipByType(int shipId, CommentType commentType)
        {
            return _context.Comments.Where(c => c.ShipId == shipId && c.CommentType == commentType && !c.IsDeleted).ToList();
        }

        public int PostComment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
            return comment.Id;
        }

        public void DeleteComment(Comment commentToDelete)
        {
            commentToDelete.IsDeleted = true;
            commentToDelete.DeleteTime = DateTime.UtcNow;
            _context.Update(commentToDelete);
            _context.SaveChanges();
        }

        public Comment? GetDeletedCommentById(int commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == commentId && c.IsDeleted);
        }

        public void RestoreComment(Comment commentToRestore)
        {
            commentToRestore.IsDeleted = false;
            commentToRestore.DeleteTime = null;
            _context.Update(commentToRestore);
            _context.SaveChanges();
        }

        public void EraseComments()
        {
            DateTime filterTime = DateTime.UtcNow.AddMinutes(-30);
            ICollection<Comment> commentsToErase = _context.Comments.Where(c => c.IsDeleted && c.DeleteTime <= filterTime).ToList();
            foreach (Comment comment in commentsToErase)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
        }

        public void EraseComment(Comment commentToErase)
        {
            _context.Comments.Remove(commentToErase);
            _context.SaveChanges();
        }
    }
}
