using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Models;
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

   
        public async Task<IActionResult> GetCategoryProducts(int? id,int page=1,string sortOrder="")
        {
            if (id == null)
            {
                return NotFound();
            }
             var categoryProducts = await _categoryService.GetCategoryProducts(id,page,18);
            switch (sortOrder)
            {
                case "price_desc":
                    categoryProducts = categoryProducts.OrderByDescending(s => s.Price).ToList();
                    
                    break;
                case "price_asc":
                    categoryProducts = categoryProducts.OrderBy(s => s.Price).ToList();
                    break;
               
                default:
                    categoryProducts = categoryProducts.OrderBy(s => s.ID).ToList();
                    break;
            }
           
            if (categoryProducts == null)
            {
                return NotFound();
            }
            var count = await _categoryService.GetCategoryProductsCount(id);

            PageViewModel pageViewModel = new PageViewModel(count, page, 12);
            ProductViewModel viewModel = new ProductViewModel
            {
                PageViewModel = pageViewModel,
                Products = categoryProducts
            };
            var categories = await _categoryService.GetAll();
            var lastProducts = await _productService.GetLastProducts(id,page);
            //var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            ViewData["lastProducts"] = lastProducts;
            ViewData["Categories"] = categories;
            
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            //ViewData["Products"] = products;
            return View("CategoryProducts", viewModel);
        }



    }
}
