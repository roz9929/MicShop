using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicShop.Core.Entities
{
    public class ProductModel
    {
        
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public double OldPrice { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="Please input a valid sku")]
        [MaxLength(18, ErrorMessage = "Please input a valid sku")]
        public string Sku { get; set; }
        [Required]
        public CategoryModel Category { get; set; }
        public string ImageBase64 { get; set; }
        
        public string Details { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public DateTime Created { get; set; }
    }
}
