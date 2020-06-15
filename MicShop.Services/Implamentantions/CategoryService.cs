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
    internal class CategoryService : ICategoryService
    {
        private readonly MicShopContext _context;
        public CategoryService(MicShopContext context)
        {
            _context = context;
        }
        public bool CategoryModelExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }


        public async Task<CategoryModel> Create(CategoryModel categoryModel)
        {
            _context.Add(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<int> Delete(int? id)
        {
            var categoryModel = await Get(id);
            _context.Category.Remove(categoryModel);
            return await _context.SaveChangesAsync();
        }

        public async Task<CategoryModel> Edit(int id, CategoryModel categoryModel)
        {
            try
            {
                _context.Category.Attach(categoryModel);
                _context.Entry(categoryModel).Property(x => x.Name).IsModified = true;
                if (categoryModel.ImageBase64 != null)
                {
                    _context.Entry(categoryModel).Property(x => x.ImageBase64).IsModified = true;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //log ex
                if (!CategoryModelExists(categoryModel.ID))
                {
                    return null;
                }

            }


            return categoryModel;

        }

        public async Task<CategoryModel> Get(int? id)
        {
            var categoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            return categoryModel;
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<List<ProductModel>> GetCategoryProducts(int? id)
        {
            var categoryModel = await _context.Category.Include(c => c.Products).FirstOrDefaultAsync(m => m.ID == id);
            List<ProductModel> productList = categoryModel.Products.ToList();
            return productList;
        }
    }
}
