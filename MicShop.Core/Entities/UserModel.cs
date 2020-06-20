using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace MicShop.Core.Entities
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is required ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone number is required ")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
    }

}
