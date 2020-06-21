using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;

namespace MicShop.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IContactService _contactService;

        public CategoryController(ICategoryService categoryService, IProductService productService, IContactService contactService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _contactService = contactService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact =  _contactService.Get();
            ViewData["contact"] = contact;
            return View();
        }

   
        public async Task<IActionResult> GetCategoryProducts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryProducts = await _categoryService.GetCategoryProducts(id);
            if (categoryProducts == null)
            {
                return NotFound();
            }
            var categories = await _categoryService.GetAll();
            var lastProducts = await _productService.GetLastProducts(id);
            //var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            ViewData["lastProducts"] = lastProducts;
            var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            ViewData["Products"] = products;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            //ViewData["Products"] = products;
            return View("CategoryProducts", categoryProducts);
        }

    }
}
