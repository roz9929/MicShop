using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    internal class ProductService : IProductService
    {
        private readonly MicShopContext _context;
        public ProductService(MicShopContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> Create(ProductModel productModel)
        {
            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<int> Delet(int id)
        {
            var productModel = await Get(id);
            _context.Product.Remove(productModel);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<ProductModel> Edit(int id, ProductModel productModel)
        {
            _context.Update(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<ProductModel> Get(int? id)
        {
           return await _context.Product.Include(category => category.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
        }

         public async Task<List<ProductModel>> GetAll()
        {
            return await _context.Product.Include(category => category.Category).ToListAsync();
        }

        public  bool ProductModelExists(int id)
        {
           return  _context.Product.Any(e => e.ID == id);
        }


    }
}
