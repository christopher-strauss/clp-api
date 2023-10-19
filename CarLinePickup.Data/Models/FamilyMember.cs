using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class FamilyMember
    {
        public FamilyMember()
        {
            Qrcodes = new HashSet<Qrcode>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid FamilyMemberTypeId { get; set; }
        public Guid? FamilyMemberTravelTypeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] ProfilePicture { get; set; }
        public Guid? EmployeeGradeId { get; set; }
        public Guid FamilyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual EmployeeGrade EmployeeGrade { get; set; }
        public virtual Family Family { get; set; }
        public virtual FamilyMemberTravelType FamilyMemberTravelType { get; set; }
        public virtual FamilyMemberType FamilyMemberType { get; set; }
        public virtual AuthenticationUser AuthenticationUser { get; set; }
        public virtual ICollection<Qrcode> Qrcodes { get; set; }
    }
}
