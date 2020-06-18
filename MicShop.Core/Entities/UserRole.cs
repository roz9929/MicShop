using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual UserModel User { get; set; }
        public virtual Role Role { get; set; }
    }
}
