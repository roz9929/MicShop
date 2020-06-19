using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
   public class OrderModel
    {
        public int ID { get; set; }
        public UserModel User { get; set; }
        public CartModel Cart { get; set; }
    }
}
