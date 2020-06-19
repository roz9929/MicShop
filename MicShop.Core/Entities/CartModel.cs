using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
    public class CartModel
    {
        public int ID { get; set; }
        public List<CartItemModel> ItemList { get; set; }
    }
}
