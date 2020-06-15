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
        private readonly MicShopContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(MicShopContext context, ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            //var products = await _productService.GetAll();
            ViewData["Categories"] = categories;
            //ViewData["Products"] = products;
            return View();
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ImageBase64")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Category.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ImageBase64")] CategoryModel categoryModel)
        {
            if (id != categoryModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
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
            //ViewData["Products"] = products;
            return View("CategoryProducts", categoryProducts);
        }
        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _context.Category.FindAsync(id);
            _context.Category.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }

        
    }
}
