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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IContactService _contactService;
        
        public ProductController(IProductService productService, ICategoryService categoryService, IContactService contactService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _contactService = contactService;
        }

        // GET: Product
        public async Task<IActionResult> OneProductById(int? id)
        {
            var categories = await _categoryService.GetAll();
            var model = await _productService.Get(id);
            var products = await _categoryService.GetCategoryProducts(model.Category.ID);
            ViewData["Categories"] = categories;
            ViewData["Products"] = products;
            var contact =  _contactService.Get();
            ViewData["contact"] = contact;

            return View("Index",model);
        }
        
    }
}
