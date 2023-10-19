
using System;

namespace CarLinePickup.API.Models.Request
{
    public class FamilyMemberRequestBase
    {
        public Guid FamilyMemberTypeId { get; set; }
        public Guid? FamilyMemberTravelTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
