using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MicShop.Core.Entities
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
