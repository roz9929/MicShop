using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;
using MicShop.Models;
using Microsoft.AspNetCore.Identity;

namespace MicShop.Controllers
{
    [Authorize]
    
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public AccountController(IUserService userService, ICategoryService categoryService, IProductService productService)
        {
            _userService=userService;
            _categoryService = categoryService;
            _productService = productService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {

            }
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            ValidateResult validateResult = _userService.Validate("Email", model.Email, model.Password);

            if (validateResult.Success)
                _userService.SignIn(this.HttpContext, validateResult.User, false);
            return this.RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            _userService.SignOut(this.HttpContext);
            return this.RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Password))
                    ModelState.AddModelError("", "Password is required");
                if (model.Password != model.ConfirmPassword)
                    ModelState.AddModelError("", "Enter the right password");

                var validate = _userService.Validate("Email", model.Email);
                if (validate.Success)
                {
                    ModelState.AddModelError("", "Email \"" + model.Email + "\" is already taken");
                }
                UserModel user = new UserModel();
                user.Email = model.Email;
                user.LastName = model.LastName;
                user.Name = model.Name;
                user.Phone = model.Phone;
                user.Address = model.Address;
                user.Created = DateTime.Now;
               
                _userService.SignUp(user, "email", user.Email, model.Password);
                _userService.AddToRole(user, "user");
            }
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            return View("Index");
        }
    }
}
