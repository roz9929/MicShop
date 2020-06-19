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

        //    // GET: Product/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var productModel = await _context.Product
        //            .FirstOrDefaultAsync(m => m.ID == id);
        //        if (productModel == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(productModel);
        //    }

        //    // GET: Product/Create
        //    public IActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: Product/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("ID,Name,Price,OldPrice,Sku,ImageBase64,Details,Created")] ProductModel productModel)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(productModel);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(productModel);
        //    }

        //    // GET: Product/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var productModel = await _context.Product.FindAsync(id);
        //        if (productModel == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(productModel);
        //    }

        //    // POST: Product/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,OldPrice,Sku,ImageBase64,Details,Created")] ProductModel productModel)
        //    {
        //        if (id != productModel.ID)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(productModel);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!ProductModelExists(productModel.ID))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(productModel);
        //    }

        //    // GET: Product/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var productModel = await _context.Product
        //            .FirstOrDefaultAsync(m => m.ID == id);
        //        if (productModel == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(productModel);
        //    }

        //    // POST: Product/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var productModel = await _context.Product.FindAsync(id);
        //        _context.Product.Remove(productModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool ProductModelExists(int id)
        //    {
        //        return _context.Product.Any(e => e.ID == id);
        //    }
        
    }
}
