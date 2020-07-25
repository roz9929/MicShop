using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MicShop.Core.Entities;
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

      

       // [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> Index(int page = 1)
        {

     //       Response.Cookies.Append(
     //    CookieRequestCultureProvider.DefaultCookieName,
     //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("am-AM")),
     //    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
     //);

            var categories = await _categoryService.GetAll();
            var count = await _productService.GetCount();
            var products = await _productService.GetAll(page,12);
            

            PageViewModel pageViewModel = new PageViewModel(count, page, 12);
            ProductViewModel viewModel = new ProductViewModel
            {
                PageViewModel = pageViewModel,
                Products = products
            };
            
            //var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View(viewModel);
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

        [HttpGet]
        public async Task<IActionResult> Wish()
        {
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View();
        }
        public async Task<IActionResult> Search(string searchString, int page= 1,string sortOrder = "")
        {
            ViewData["searchString"] = searchString;
            Random rnd = new Random();
            List<ProductModel> searchProducts = new List<ProductModel>();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                searchProducts = await _productService.Search(searchString);
            }
            switch (sortOrder)
            {
                case "price_desc":
                    searchProducts = searchProducts.OrderByDescending(s => s.Price).ToList();

                    break;
                case "price_asc":
                    searchProducts = searchProducts.OrderBy(s => s.Price).ToList();
                    break;

                default:
                    searchProducts = searchProducts.OrderBy(s => s.ID).ToList();
                    break;
            }

            if (searchProducts == null)
            {
                return NotFound();
            }
            PageViewModel pageViewModel = new PageViewModel(searchProducts.Count, page, 12);
            ProductViewModel viewModel = new ProductViewModel
            {
                PageViewModel = pageViewModel,
                Products = searchProducts.Skip((page - 1) * 12).Take(12),
            };
            var lastProducts = await _productService.GetLastProducts(searchProducts[0].Category.ID, page);
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            ViewData["lastProducts"] = lastProducts;
            return View("SearchResoultView",viewModel);
        }

        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }
    }
}
