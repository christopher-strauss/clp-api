using System;

namespace CarLinePickup.Domain.Models
{
    public class FamilyMember : ModelBase
    {
        public AuthenticationUser AuthenticationUser { get; set; }
        public Employee Employee { get; set; }
        public Guid FamilyMemberTypeId { get; set; }
        public string FamilyMemberType { get; set; }
        public Guid? FamilyMemberTravelTypeId { get; set; }
        public string FamilyMemberTravelType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
