using SpottingBlogpost.Data;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Models;
using SpottingBlogpost.Services.Interfaces;

namespace SpottingBlogpost.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SpottingContext _context;
        private readonly IShipService _shipService;
        private readonly ICommentService _commentService;

        public UserService(SpottingContext context, IShipService shipService, ICommentService commentService)
        {
            _context = context;
            _shipService = shipService;
            _commentService = commentService;
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId && !u.IsDeleted);
        }

        public List<User> GetUsersByType(string userType)
        {
            return _context.Users.Where(t => t.UserType == userType && !t.IsDeleted).ToList();
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public BaseResponse ValidarUsuario(string email, string password)
        {
            BaseResponse response = new BaseResponse();
            User? userForLogin = _context.Users.SingleOrDefault(u => u.Email == email);
            if (userForLogin != null && !userForLogin.IsDeleted)
            {
                if (userForLogin.Password == password)
                {
                    response.Result = true;
                    response.Message = "loging Succesfull";
                }
                else
                {
                    response.Result = false;
                    response.Message = "wrong password";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "wrong email or deleted user";
            }
            return response;
        }

        public int CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public void UpdateUser(User userToUpdate)
        {
            _context.Update(userToUpdate);
            _context.SaveChanges();
        }
        public User? GetUserForDeletionById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId && !u.IsDeleted && u.UserType != "Admin");
        }

        public void DeleteUser(User userToDelete)
        {
            userToDelete.IsDeleted = true;
            userToDelete.DeleteTime = DateTime.UtcNow;

            ICollection<Comment> commentsToCascadeDelete = _context.Comments.Where(c => c.PosterId == userToDelete.Id && !c.IsDeleted && c != null).ToList();
            foreach (Comment comment in commentsToCascadeDelete)
            {
                _commentService.DeleteComment(comment);
            }

            ICollection<Ship> shipsToCascadeDelete = _context.Ships.Where(s => s.SpotterId == userToDelete.Id && !s.IsDeleted && s != null).ToList();
            foreach (Ship ship in shipsToCascadeDelete)
            {
                _shipService.DeleteShip(ship);
            }

            _context.Update(userToDelete);
            _context.SaveChanges();
        }

        public User? GetDeletedUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId && u.IsDeleted);
        }

        public void RestoreUser(User userToRestore)
        {
            userToRestore.IsDeleted = false;
            userToRestore.DeleteTime = null;
            _context.Update(userToRestore);
            _context.SaveChanges();
        }

        public void CascadeRestoreUser(User userToRestore)
        {
            DateTime deletedTime = (DateTime)userToRestore.DeleteTime;

            RestoreUser(userToRestore);

            ICollection<Comment> commentsToCascadeRestore = _context.Comments.Where(c => c.PosterId == userToRestore.Id && c.DeleteTime >= deletedTime && c != null).ToList();
            foreach (Comment comment in commentsToCascadeRestore)
            {
                _commentService.RestoreComment(comment);
            }

            ICollection<Ship> shipsToCascadeRestore = _context.Ships.Where(s => s.SpotterId == userToRestore.Id && s.DeleteTime >= deletedTime && s != null).ToList();
            foreach (Ship ship in shipsToCascadeRestore)
            {
                _shipService.CascadeRestoreShip(ship);
            }

        }

        public void EraseUsers()
        {
            DateTime filterTime = DateTime.UtcNow.AddMinutes(-30);             
            ICollection<User> usersToErase = _context.Users.Where(u => u.IsDeleted && u.DeleteTime <= filterTime).ToList();
            foreach (User user in usersToErase)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void EraseUser(User userToErase)
        {
            _context.Users.Remove(userToErase);
            _context.SaveChanges();
        }
    }
}
