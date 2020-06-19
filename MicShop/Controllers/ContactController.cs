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

namespace MicShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly ICategoryService _categoryService;

        public ContactController(IContactService contactService,ICategoryService categoryService)
        {
            _contactService = contactService;
            _categoryService = categoryService;
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var contact =  _contactService.Get();
            ViewData["contact"] = contact;
            var categories = await _categoryService.GetAll();
            ViewData["Categories"] = categories;
            return View(contact);
        }

        //// GET: Contact/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var contactModel = await _context.Contact
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (contactModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contactModel);
        //}

        //// GET: Contact/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Contact/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Phone,Address,Email,OpenFrom,OpenTo,FaceBook,Instagram,Twitter")] ContactModel contactModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contactModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contactModel);
        //}

        //// GET: Contact/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var contactModel = await _context.Contact.FindAsync(id);
        //    if (contactModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contactModel);
        //}

        //// POST: Contact/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Phone,Address,Email,OpenFrom,OpenTo,FaceBook,Instagram,Twitter")] ContactModel contactModel)
        //{
        //    if (id != contactModel.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contactModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactModelExists(contactModel.ID))
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
        //    return View(contactModel);
        //}

        //// GET: Contact/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var contactModel = await _context.Contact
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (contactModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contactModel);
        //}

        //// POST: Contact/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var contactModel = await _context.Contact.FindAsync(id);
        //    _context.Contact.Remove(contactModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactModelExists(int id)
        //{
        //    return _context.Contact.Any(e => e.ID == id);
        //}
    }
}
