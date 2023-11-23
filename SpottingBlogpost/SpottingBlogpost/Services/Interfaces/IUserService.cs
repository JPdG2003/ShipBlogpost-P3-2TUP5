using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Models;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface IUserService
    {
        public BaseResponse ValidarUsuario(string username, string password);
        public User? GetUserById(int userId);
        public List<User> GetUsersByType(string userType);
        public User? GetUserByEmail(string username);
        public int CreateUser(User user);
        public void UpdateUser(User userToUpdate);
        public User? GetUserForDeletionById(int userId);
        public void DeleteUser(User userToDelete);
        public User? GetDeletedUserById(int userId);
        public void RestoreUser(User userToRestore);
        public void CascadeRestoreUser(User userToRestore);
        public void EraseUsers();
        public void EraseUser(User userToErase);
    }
}
