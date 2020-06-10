//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MicShop.Models;

//namespace MicShop.Controllers
//{
//    public class ProductController : Controller
//    {
//        private readonly MicShopContext _context;

//        public ProductController(MicShopContext context)
//        {
//            _context = context;
//        }

//        // GET: Product
//        public async Task<IActionResult> Index()
//        {
            
//            return View(await _context.Product.Include(category => category.Category).ToListAsync());
//        }

//        // GET: Product/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var productModel = await _context.Product.Include(category => category.Category)
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (productModel == null)
//            {
//                return NotFound();
//            }

//            return View(productModel);
//        }

//        // GET: Product/Create
//        public IActionResult Create()
//        {
//            var categories = _context.Category.ToList();
//            ViewBag.Categories = new SelectList(categories, "ID", "Name");
//            return View();
//        }

//        // POST: Product/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(ProductModel productModel)
//        {
//            if (ModelState.IsValid)
//            {
                
//                var cat = await _context.Category.FirstOrDefaultAsync(m => m.ID == productModel.Category.ID);
//                productModel.Category = cat;
//                MemoryStream ms = new MemoryStream();
//                await productModel.Image.CopyToAsync(ms);
//                string base64Image = Convert.ToBase64String(ms.ToArray());
//                productModel.ImageBase64 = base64Image;
//                _context.Add(productModel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(productModel);
//        }

//        // GET: Product/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var categories = _context.Category.ToList();
//            ViewBag.Categories = new SelectList(categories, "ID", "Name");

//            var productModel = await _context.Product.FindAsync(id);
//            if (productModel == null)
//            {
//                return NotFound();
//            }
//            return View(productModel);
//        }

//        // POST: Product/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, ProductModel productModel)
//        {
//            if (id != productModel.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var cat = await _context.Category.FirstOrDefaultAsync(m => m.ID == productModel.Category.ID);
//                    productModel.Category = cat;
//                    MemoryStream ms = new MemoryStream();
//                    if (productModel.Image != null)
//                    {
//                        await productModel.Image.CopyToAsync(ms);
//                        string base64Image = Convert.ToBase64String(ms.ToArray());
//                        productModel.ImageBase64 = base64Image;
//                    }
//                    _context.Update(productModel);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ProductModelExists(productModel.ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(productModel);
//        }

//        // GET: Product/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var productModel = await _context.Product
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (productModel == null)
//            {
//                return NotFound();
//            }

//            return View(productModel);
//        }

//        // POST: Product/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var productModel = await _context.Product.FindAsync(id);
//            _context.Product.Remove(productModel);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ProductModelExists(int id)
//        {
//            return _context.Product.Any(e => e.ID == id);
//        }
//    }
//}
