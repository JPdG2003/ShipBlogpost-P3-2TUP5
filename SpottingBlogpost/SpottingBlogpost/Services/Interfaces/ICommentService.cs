using SpottingBlogpost.Data.Entities;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface ICommentService
    {
        public Comment? GetCommentById(int id);
        public List<Comment> GetAllCommentsByShipId(int shipId);
        public List<Comment> GetAllCommentsByPoster(int posterId);
        public int PostComment(Comment comment);
        public void DeleteComment(Comment commentToDelete);

    }
}
