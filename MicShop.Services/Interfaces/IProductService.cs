using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAll(int page,int pageSize);
        Task<ProductModel> Get(int? id);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Edit(int id, ProductModel productModel);
        Task<int> Delet(int id);
        bool ProductModelExists(int id);
        Task<List<ProductModel>> GetProductsByCategory(int? id,int page);
        Task<List<ProductModel>> GetLastProducts(int? id,int page);
        Task<List<ProductModel>> GetProductsByIdList(List<int> IdList);
        Task<int> GetCount();
        Task<List<ProductModel>> Search(string searchString);
    }
}

