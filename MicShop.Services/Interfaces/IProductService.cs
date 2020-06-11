using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAll();
        Task<ProductModel> Get(int? id);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Edit(int id, ProductModel productModel);
        Task<int> Delet(int id);
        bool ProductModelExists(int id);
    }
}

