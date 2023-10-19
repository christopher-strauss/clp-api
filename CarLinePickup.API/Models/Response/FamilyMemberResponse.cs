using System;


namespace CarLinePickup.API.Models.Response
{
    public class FamilyMemberResponse : ResponseBase
    {
        public EmployeeResponse Employee { get; set; }
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
