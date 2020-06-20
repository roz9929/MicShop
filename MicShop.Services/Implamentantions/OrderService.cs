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
    public class OrderService:IOrderService
    {
        private readonly MicShopContext _context;
        private readonly IUserService _userService;

        public OrderService(MicShopContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<OrderModel> Create(CartModel cartModel, UserModel currentUser)
        {
            OrderModel ordermodel = new OrderModel();
            ordermodel.Cart = cartModel;
            ordermodel.User = currentUser;
            _context.Add(ordermodel);
            await _context.SaveChangesAsync();
            return ordermodel;
        }

        

        public async Task<List<OrderModel>> GetAll()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<OrderModel> GetOrderById(int? id)
        {
            
                return await _context.Order.FirstOrDefaultAsync(m => m.ID == id);
            
        }

        public async Task<List<OrderModel>> GetOrdersByUserId(int id)
        {

            return await _context.Order.Where(s => s.ID == id).ToListAsync();
        }

    }
}
