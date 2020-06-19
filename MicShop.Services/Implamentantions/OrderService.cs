using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Implamentantions
{
    public class OrderService:IOrderService
    {
        private readonly MicShopContext _context;

        public OrderService(MicShopContext context)
        {
            _context = context;
        }

        public async Task<List<OrderModel>> GetAll()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<OrderModel> GetOrderById(int? id)
        {
            
                return await _context.Order.FirstOrDefaultAsync(m => m.ID == id);
            
        }
    }
}
