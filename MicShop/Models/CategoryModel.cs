using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MicShop.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
