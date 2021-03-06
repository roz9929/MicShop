﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Models;
using MicShop.Services.Interfaces;

namespace MicShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly IContactService _contactService;
        private readonly IProductService _productService;
        public OrderController(IOrderService orderService, IProductService productService, IContactService contactService, IUserService userService, ICategoryService categoryService)
        {
            _orderService = orderService;
            _userService = userService;
            _categoryService = categoryService;
            _contactService = contactService;
            _productService = productService;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var user = _userService.GetCurrentUser(HttpContext);
            var userOrders = _orderService.GetOrdersByUserId(user.ID);
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            return View();
        }
        //POST: Order/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuth(CartModel cart, string OrderNotes)
        {
            DoneModelView done = new DoneModelView();
            if (ModelState.IsValid)
            {

                if(!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index");
                }

                UserModel user = _userService.GetCurrentUser(HttpContext);
                var order = await _orderService.Create(cart, user, OrderNotes);
                var products = await _productService.GetProductsByIdList(order.Cart.ItemList.Select(id => id.ProductId).ToList());
                done.order = order;
                done.products = products;
            }

            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View("Done",done);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CartModel cart, OrderUserViewModel model,string OrderNotes)
        {
            DoneModelView done = new DoneModelView();
            if (ModelState.IsValid)
            {

                
                var validate = _userService.Validate("Email", model.Email);
                if (validate.Success)
                {
                    ModelState.AddModelError("", "Email \"" + model.Email + "\" is already taken");
                }
                UserModel user = new UserModel();
                user.Email = model.Email;
                user.LastName = model.LastName;
                user.Name = model.Name;
                user.Phone = model.Phone;
                user.Address = model.Address;
                user.Created = DateTime.Now;
                if (model.CreateAccount == true)
                {
                    if (string.IsNullOrWhiteSpace(model.Password))
                        ModelState.AddModelError("", "Password is required");
                    if (model.Password != model.ConfirmPassword)
                        ModelState.AddModelError("", "Enter the right password");

                    _userService.SignUp(user, "email", user.Email, model.Password);
                    _userService.AddToRole(user, "user");

                }

                var order=await _orderService.Create(cart, user, OrderNotes);
                var products = await _productService.GetProductsByIdList(order.Cart.ItemList.Select(id => id.ProductId).ToList());
                done.order = order;
                done.products = products;
            }

            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View("Done",done);
        }
        public async Task<IActionResult> CheckOut()
        {
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            var contact = _contactService.Get();
            ViewData["contact"] = contact;
            return View();
        }
        

        //// GET: Order/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}



        //// GET: Order/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderModel = await _context.Order.FindAsync(id);
        //    if (orderModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(orderModel);
        //}

        //// POST: Order/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID")] OrderModel orderModel)
        //{
        //    if (id != orderModel.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(orderModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderModelExists(orderModel.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderModel);
        //}

        //// GET: Order/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderModel = await _context.Order
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (orderModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orderModel);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var orderModel = await _context.Order.FindAsync(id);
        //    _context.Order.Remove(orderModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderModelExists(int id)
        //{
        //    return _context.Order.Any(e => e.ID == id);
        //}
    }
}
