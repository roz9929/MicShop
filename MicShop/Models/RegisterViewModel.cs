using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicShop.Models
{
    public class RegisterViewModel 
    {
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
        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Enter the right password")]
        public string ConfirmPassword { get; set; }
        
    }
}
