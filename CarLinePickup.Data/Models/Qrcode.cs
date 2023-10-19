using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Qrcode
    {
        public Guid Id { get; set; }
        public Guid? FamilyMemberId { get; set; }
        public byte[] Qrcode1 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }

        public virtual FamilyMember FamilyMember { get; set; }
    }
}
