using SpottingBlogpost.Data;
using SpottingBlogpost.Data.Entities;
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

        public List<Comment> GetAllCommentsByShipId(int shipId)
        {
            return _context.Comments
                .Where(s => s.ShipId == shipId)
                .ToList();
        }

        public List<Comment> GetAllCommentsByPoster(int posterId)
        {
            return _context.Comments
                .Where(p => p.PosterId == posterId)
                .ToList();
        }

        public int PostComment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
            return comment.Id;
        }

        public void DeleteComment (int commentId)
        {
            Comment? commentToDelete = _context.Comments.FirstOrDefault(c => c.Id == commentId);
            commentToDelete.IsDeleted = true;
            _context.Update(commentToDelete);
            _context.SaveChanges();
        }
    }
}
