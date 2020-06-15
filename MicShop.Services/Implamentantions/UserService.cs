using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Implamentantions
{
    public class UserService : IUserService
    {
        private readonly MicShopContext _context;
        public UserService(MicShopContext context)
        {
            _context = context;
        }


        public async Task<UserModel> Authenticate(string userName, string password)
        {
            UserModel user = await _context.User.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password);
            if (user == null)
                return null;
            user.Password = null;
            return user;
        }

        public Task Authenticate(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Register(UserModel usermodel)
        {
            throw new NotImplementedException();
        }

  
    }
}
