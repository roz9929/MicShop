using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
   public interface IOrderService
    {
        Task<List<OrderModel>> GetAll();
        Task<OrderModel> GetOrderById(int? id);

    }
}
