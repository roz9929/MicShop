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
        public string OrderNotes { get; set; }
        public DateTime CreationDate { get; set; }
        //public ReviewModel review { get; set; }
    }
}
