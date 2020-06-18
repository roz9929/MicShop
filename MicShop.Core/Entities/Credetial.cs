using System;
using System.Collections.Generic;
using System.Text;

namespace MicShop.Core.Entities
{
    public class Credential
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CredentialTypeId { get; set; }
        public string Identifier { get; set; }
        public string Secret { get; set; }
        public string Extra { get; set; }

        public virtual UserModel User { get; set; }
        public virtual CredentialType CredentialType { get; set; }
    }
}
