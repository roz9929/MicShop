using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Implamentantions
{
    class MenuItemsService : IMenuItemsService
    {
        private readonly MicShopContext _context;
        public MenuItemsService(MicShopContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(MenuItemsModel menuItemsModel)
        {
            _context.Add(menuItemsModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MenuItemsModel> Get(int? id)
        {
           var menuItem= await _context.MenuItems
                .FirstOrDefaultAsync(m => m.ID == id);
            return menuItem;
        }

        public async Task<bool> Edit(MenuItemsModel menuItemsModel)
        {
            _context.Update(menuItemsModel);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<MenuItemsModel>> GetAll()
        {
            return await _context.MenuItems.ToListAsync();
        }
        
        public bool MenuItemsModelExists(int id)
        {
            return _context.MenuItems.Any(e => e.ID == id);
        }

        public async Task<bool> Delete(MenuItemsModel menuItemsModel)
        {
            _context.MenuItems.Remove(menuItemsModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MenuItemsModel> GetByName(string name)
        {
            var menuItem = await _context.MenuItems
                .FirstOrDefaultAsync(m => m.Name == name);
            return menuItem;
        }

    }
}
