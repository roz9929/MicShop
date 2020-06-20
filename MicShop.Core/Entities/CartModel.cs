using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MicShop.Core.Entities
{
    public class CartModel
    {
        public int ID { get; set; }
        [Required]
        public List<CartItemModel> ItemList { get; set; }
    }
}
