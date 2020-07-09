using MicShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicShop.Models
{
    public class DoneModelView
    {
        public OrderModel order { get; set; }
        public List<ProductModel> products { get; set; }

    }
}
