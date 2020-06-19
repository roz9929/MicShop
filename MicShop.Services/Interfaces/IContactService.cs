using Microsoft.AspNetCore.Mvc;
using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicShop.Services.Interfaces
{
    public interface IContactService
    {
        ContactModel Get();
        Task<List<ContactModel>> GetAll();
        Task<ContactModel> Create(ContactModel contactModel);
        Task<ContactModel> GetById(int? id);
        Task<ContactModel> Edit(int id, ContactModel contactModel);
        bool ContactModelExists(int id);
        Task<bool> Delete(int? id);
    }
}
