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

        public int PostComment(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
            return comment.Id;
        }

        public void DeleteComment (Comment commentToDelete)
        {
            commentToDelete.IsDeleted = true;
            _context.Update(commentToDelete);
            _context.SaveChanges();
        }
    }
}
