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
    public class ContactAdminController : Controller
    {
        private readonly IContactService _contactService;

        public ContactAdminController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: ContactAdmin
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAll();
            return View(contacts);
        }

        // GET: ContactAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contactModel = await _contactService.GetById(id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // GET: ContactAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Phone", "Address", "Email", "OpenFrom", "OpenTo", "FaceBook", "Instagram", "Twitter")]ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {

                var contactmodel =await _contactService.Create(contactModel);
                return RedirectToAction(nameof(Index));
            }
            return View(contactModel);
        }

        // GET: ContactAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _contactService.GetById(id);
            if (contactModel == null)
            {
                return NotFound();
            }
            return View(contactModel);
        }

        // POST: ContactAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactModel contactModel)
        {
            if (id != contactModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactService.Edit(id, contactModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_contactService.ContactModelExists(contactModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contactModel);
        }

        // GET: ContactAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _contactService.GetById(id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // POST: ContactAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _contactService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

   
    }
}
