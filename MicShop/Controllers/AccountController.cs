using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicShop.Core.Data;
using MicShop.Core.Entities;
using MicShop.Services.Interfaces;
using MicShop.ViewModels;

namespace MicShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService=userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.Authenticate(model.Email,model.Password);
               
                if (user != null)
                {
                   return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong data");
            }
            return View(model);
        }

        //// GET: Account
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.User.ToListAsync());
        //}

        //// GET: Account/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _context.User
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userModel);
        //}

        //// GET: Account/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Account/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModel userModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userModel);
        //}

        //// GET: Account/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _context.User.FindAsync(id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userModel);
        //}

        //// POST: Account/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LastName,Phone,Email,Password")] UserModel userModel)
        //{
        //    if (id != userModel.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserModelExists(userModel.ID))
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
        //    return View(userModel);
        //}

        //// GET: Account/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userModel = await _context.User
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (userModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userModel);
        //}

        //// POST: Account/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var userModel = await _context.User.FindAsync(id);
        //    _context.User.Remove(userModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserModelExists(int id)
        //{
        //    return _context.User.Any(e => e.ID == id);
        //}
    }
}
