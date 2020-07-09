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

namespace MicShop.Admin.Controllers
{
    public class PageController : Controller
    {
        private readonly IMenuItemsService _menuItemsService;

        public PageController(IMenuItemsService menuItemsService)
        {
            _menuItemsService = menuItemsService;
        }

        // GET: MenuItems
        public async Task<IActionResult> AdminIndex()
        {
            return View("Index",await _menuItemsService.GetAll());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _menuItemsService.Get(id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemsModel menuItemsModel)
        {
            if (ModelState.IsValid)
            {
               await _menuItemsService.Create(menuItemsModel);
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(menuItemsModel);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemsModel = await _menuItemsService.Get(id);
            if (menuItemsModel == null)
            {
                return NotFound();
            }
            return View(menuItemsModel);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  MenuItemsModel menuItemsModel)
        {
            if (id != menuItemsModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _menuItemsService.Edit(menuItemsModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_menuItemsService.MenuItemsModelExists(menuItemsModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(menuItemsModel);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemsModel = await _menuItemsService.Get(id);
            if (menuItemsModel == null)
            {
                return NotFound();
            }

            return View(menuItemsModel);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemsModel = await _menuItemsService.Get(id);
            await _menuItemsService.Delete(menuItemsModel);
            return RedirectToAction(nameof(AdminIndex));
        }

       
    }
}
