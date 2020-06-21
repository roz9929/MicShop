using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;

namespace MicShopAdmin.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetAll());
        }

        //GET: ContactAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orderModel = await _orderService.GetOrderById(id);
                
            if (orderModel == null)
            {
                return NotFound();
            }

            var orderProductList = await _productService.GetProductsByIdList(orderModel.Cart.ItemList.Select(e=>e.ProductId).ToList());
            ViewData["orderModel"] = orderModel;
            return View(orderProductList);
        }
        
    }
}
