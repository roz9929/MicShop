using Microsoft.AspNetCore.Mvc;
using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
    public interface IMenuItemsService
    {

        Task<List<MenuItemsModel>> GetAll();
        Task<MenuItemsModel> Get(int? id);
        Task<bool> Create(MenuItemsModel menuItemsModel);
        Task<bool> Edit(MenuItemsModel menuItemsModel);
        bool MenuItemsModelExists(int id);
        Task<bool> Delete(MenuItemsModel menuItemsModel);
        Task<MenuItemsModel> GetByName(string name);

    }
}
