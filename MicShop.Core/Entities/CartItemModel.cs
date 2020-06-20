using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
    public class CartItemModel
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
