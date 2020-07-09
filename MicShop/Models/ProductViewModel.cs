using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicShop.Models
{
    public class ProductViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
