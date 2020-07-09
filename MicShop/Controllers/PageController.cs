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
    public class PageController : Controller
    {
        private readonly IMenuItemsService _menuItemsService;
        private readonly ICategoryService _categoryService;
        private readonly IContactService _contactService;


        public PageController(IMenuItemsService menuItemsService, ICategoryService categoryService, IContactService contactService)
        {
            _menuItemsService = menuItemsService;
            _contactService = contactService;
            _categoryService = categoryService;
        }

        // GET: Page
        public async Task<IActionResult> Index(string name)
        {

            var menuItem = await _menuItemsService.GetByName(name);
            var categories = await _categoryService.GetAll();
            var contact =  _contactService.Get();
            ViewData["contact"] = contact;
            ViewData["Categories"] = categories;
            return View("Index", menuItem);
        }

        //// GET: Page/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var menuItemsModel = await _context.MenuItems
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (menuItemsModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(menuItemsModel);
        //}

        //// GET: Page/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Page/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Name,Text")] MenuItemsModel menuItemsModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(menuItemsModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(menuItemsModel);
        //}

        //// GET: Page/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var menuItemsModel = await _context.MenuItems.FindAsync(id);
        //    if (menuItemsModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(menuItemsModel);
        //}

        //// POST: Page/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Text")] MenuItemsModel menuItemsModel)
        //{
        //    if (id != menuItemsModel.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(menuItemsModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MenuItemsModelExists(menuItemsModel.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(menuItemsModel);
        //}

        //// GET: Page/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var menuItemsModel = await _context.MenuItems
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (menuItemsModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(menuItemsModel);
        //}

        //// POST: Page/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var menuItemsModel = await _context.MenuItems.FindAsync(id);
        //    _context.MenuItems.Remove(menuItemsModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MenuItemsModelExists(int id)
        //{
        //    return _context.MenuItems.Any(e => e.ID == id);
        //}
    }
}
