using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class AuthenticationUser
    {
        public Guid Id { get; set; }
        public Guid? FamilyMemberId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid AuthenticationUserProviderId { get; set; }
        public Guid AuthenticationUserTypeId { get; set; }
        public string ExternalProviderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }

        public virtual AuthenticationUserProvider AuthenticationUserProvider { get; set; }
        public virtual AuthenticationUserType AuthenticationUserType { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual FamilyMember FamilyMember { get; set; }
    }
}
