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
    public class CategoryController : Controller
    {
        //private readonly 
        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAll());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _categoryService.Get(id);
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
        public async Task<IActionResult> Create(CategoryModel categoryModel)
        {
            if (categoryModel.Image == null)
            {
                ModelState.AddModelError("Image", "Please Select Image");
            }
            if (categoryModel.Name == null)
            {
                ModelState.AddModelError("Name", "Please enter name");
            }

            if (ModelState.IsValid)
            {

                categoryModel.ImageBase64 = await SetImageIntoModel(categoryModel);
                await _categoryService.Create(categoryModel);
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

            var categoryModel = await _categoryService.Get(id);
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
        public async Task<IActionResult> Edit(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.ID)
            {
                return NotFound();
            }

            if (categoryModel.Name == null)
            {
                ModelState.AddModelError("Name", "Please enter name");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryModel.Image != null)
                    {
                        categoryModel.ImageBase64 = await SetImageIntoModel(categoryModel);
                    }



                    await _categoryService.Edit(id, categoryModel);
                }
                catch (Exception ex)
                {
                    //log exeption
                    return NotFound();
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

            var categoryModel = await _categoryService.Get(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private async Task<string> SetImageIntoModel(CategoryModel categoryModel)
        {
            MemoryStream ms = new MemoryStream();
            await categoryModel.Image.CopyToAsync(ms);
            string base64Image = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            return base64Image;

        }
    }
}
