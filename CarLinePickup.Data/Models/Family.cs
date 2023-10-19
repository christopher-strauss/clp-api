using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Family
    {
        public Family()
        {
            FamilyMembers = new HashSet<FamilyMember>();
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
