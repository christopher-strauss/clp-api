using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class AuthenticationUserType
    {
        public AuthenticationUserType()
        {
            AuthenticationUsers = new HashSet<AuthenticationUser>();
        }

        public Guid Id { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<AuthenticationUser> AuthenticationUsers { get; set; }
    }
}
