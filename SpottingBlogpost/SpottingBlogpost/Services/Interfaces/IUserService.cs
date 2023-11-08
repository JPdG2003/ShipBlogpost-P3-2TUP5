﻿using SpottingBlogpost.Data.Entities;
using SpottingBlogpost.Data.Models;

namespace SpottingBlogpost.Services.Interfaces
{
    public interface IUserService
    {
        public BaseResponse ValidarUsuario(string username, string password);
        public User? GetUserByEmail(string username);
        public int CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int userId);
    }
}