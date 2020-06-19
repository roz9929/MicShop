using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MicShop.Models;
using MicShop.Services.Interfaces;

namespace MicShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IContactService _contactService;


        public ShopController(ILogger<ShopController> logger, IContactService contactService, ICategoryService categoryService,IProductService productService, IUserService userService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
            _userService = userService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            ViewData["Products"] = products;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Basket()
        {
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View();
        }
    }
}
