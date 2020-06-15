using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
    public interface ICategoryService
    {

        Task<List<CategoryModel>> GetAll();
        Task<CategoryModel> Get(int? id);
        Task<CategoryModel> Create(CategoryModel categoryModel);
        Task<CategoryModel> Edit(int id, CategoryModel categoryModel);
        Task<int> Delete(int? id);
      Task<List<ProductModel>> GetCategoryProducts(int? id);

    }
}
