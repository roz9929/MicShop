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
        private readonly ICategoryService _categoryService;
       

        public ProductService(MicShopContext context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }
        public async Task<ProductModel> Create(ProductModel productModel)
        {
            productModel.Created = DateTime.Now;
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
            _context.Entry(productModel).State = EntityState.Modified;

            if (productModel.ImageBase64 == null)
            {
                _context.Entry(productModel).Property(x => x.ImageBase64).IsModified = false;
            }
  
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<ProductModel> Get(int? id)
        {
           return await _context.Product.Include(category => category.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<int> GetCount()
        {
            IQueryable<ProductModel> source = _context.Product.Include(category => category.Category);
            var count = await source.CountAsync();
            return count;
        }
         public async Task<List<ProductModel>> GetAll(int page,int pageSize)
        {
            IQueryable<ProductModel> source = _context.Product.Include(category => category.Category);
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return items;
        }

        public  bool ProductModelExists(int id)
        {
           return  _context.Product.Any(e => e.ID == id);
        }

        public async Task<List<ProductModel>> GetProductsByCategory(int? id, int page=1)
        {
            List<ProductModel> categoryProducts = new List<ProductModel>();
            categoryProducts = await _categoryService.GetCategoryProducts(id,page,3);
            return categoryProducts;

        }

        public async Task<List<ProductModel>> GetLastProducts(int? id,int page=1)
        {
            List<ProductModel> categoryProducts = new List<ProductModel>();
            categoryProducts = await _categoryService.GetCategoryProducts(id,page,3);
            List<ProductModel> lastProducts = new List<ProductModel>();
            lastProducts = categoryProducts.OrderBy(x => x.Created).ToList();
            if (lastProducts.Count > 9)
                lastProducts = lastProducts.GetRange(0, 9);

            return lastProducts;

        }
        
        public async Task<List<ProductModel>> GetProductsByIdList(List<int> IdList)
        {
            var prodList= await _context.Product.ToListAsync();
            return  prodList.Where(x=>IdList.Contains(x.ID)).ToList();
        }

        public async Task<List<ProductModel>> Search(string searchString)
        {

            var products = await _context.Product.Include(category => category.Category).ToListAsync();
            var searchProducts = products.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            return searchProducts;
        }
    }
}
