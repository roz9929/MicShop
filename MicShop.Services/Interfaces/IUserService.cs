using Microsoft.AspNetCore.Mvc;
using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MicShop.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string userName, string password);
        Task<IActionResult> Register(UserModel usermodel);
        Task Authenticate(string userName);
        Task<IActionResult> Logout();
    }
}
