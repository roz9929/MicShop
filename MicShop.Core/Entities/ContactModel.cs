using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
    public class ContactModel
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int OpenFrom { get; set; }
        public int OpenTo { get; set; }
        public string FaceBook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }

    }
}
