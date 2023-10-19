using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class FamilyMemberTravelType
    {
        public FamilyMemberTravelType()
        {
            FamilyMembers = new HashSet<FamilyMember>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<FamilyMember> FamilyMembers { get; set; }
    }
}
