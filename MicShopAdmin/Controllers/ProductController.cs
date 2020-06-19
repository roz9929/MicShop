using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Entities;
using MicShop.Models;
using MicShop.Services.Interfaces;

namespace MicShop.Controllers
{
  
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {

            return View(await _productService.GetAll() );
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productService.Get(id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Product/Create
       async public Task<IActionResult> Create()
        {
            var categories =await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            if (productModel.Image == null)
            {
                ModelState.AddModelError("Image", "Please Select Image");
            }
            if (ModelState.IsValid)
            {

                var cat = await _categoryService.Get(productModel.Category.ID);
                productModel.Category = cat;
                string base64Image = await SetImageIntoModel(productModel);
                productModel.ImageBase64 = base64Image;
                productModel = await _productService.Create(productModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name");

            var productModel = await _productService.Get(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price,OldPrice,Sku,Image,Category")] ProductModel productModel)
        {
            if (id != productModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cat = await _categoryService.Get(productModel.Category.ID);
                    productModel.Category = cat;
                    
                    if (productModel.Image != null)
                    {

                        string base64Image = await SetImageIntoModel(productModel);
                        productModel.ImageBase64 = base64Image;
                    }
                    productModel = await _productService.Edit(id, productModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productService.ProductModelExists(productModel.ID))
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
            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _productService.Get(id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Delet(id);
            
            return RedirectToAction(nameof(Index));
        }

       
        private async Task<string> SetImageIntoModel(ProductModel productModel)
        {
            MemoryStream ms = new MemoryStream();
            await productModel.Image.CopyToAsync(ms);
            string base64Image = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return base64Image;

        }


        public class IdList
        {
            public List<int> idlist { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductsByIdList([FromBody] IdList idList)
        {
            List<ProductModel> productsList = await _productService.GetProductsByIdList(idList.idlist);
            return Json(productsList);
        }

        
    }
}
