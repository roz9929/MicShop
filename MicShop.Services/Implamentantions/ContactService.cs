

using Microsoft.AspNetCore.Mvc;
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
   public class ContactService:IContactService
    {
        private readonly MicShopContext _context;

        public ContactService(MicShopContext context)
        {
            _context = context;
        }

        public bool ContactModelExists(int id)
        {
            return _context.Contact.Any(e => e.ID == id);
        }

        public async Task<ContactModel> Create(ContactModel contactModel)
        {
            _context.Add(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public async Task<bool> Delete(int? id)
        {
            var contactModel = await GetById(id);
            _context.Contact.Remove(contactModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ContactModel> Edit(int id, ContactModel contactModel)
        {
            _context.Update(contactModel);
            await _context.SaveChangesAsync();
            return contactModel;
        }

        public ContactModel Get()
        {
            var categories = _context.Contact.ToList();
            return  categories.LastOrDefault();
        }

        public async Task<List<ContactModel>> GetAll()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<ContactModel> GetById(int? id)
        {
          return  await _context.Contact.FindAsync(id);
        }
    }
}
