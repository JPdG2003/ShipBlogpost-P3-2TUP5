using SpottingBlogpost.Data;
using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Models;
using SpottingBlogpost.Services.Interfaces;

namespace SpottingBlogpost.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SpottingContext _context;

        public UserService(SpottingContext context)
        {
            _context = context;
        }

        public User? GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId && !u.IsDeleted);
        }

        public List<User> GetUsersByType(string userType) 
        {
            return _context.Users.Where(t =>  t.UserType == userType && !t.IsDeleted).ToList();
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

        public void DeleteUser (int userId)
        {
            User userToDelete = _context.Users.FirstOrDefault(u => u.Id == userId);
            userToDelete.IsDeleted = true;
            _context.Update(userToDelete);
            _context.SaveChanges();
        }

    }
}
