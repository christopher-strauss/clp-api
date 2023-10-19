using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class FamilyMemberRegistrationRequestCreate : FamilyMemberRequestBase, IRequestCreate
    {
        public AuthenticationUserRequestCreate AuthenticationUser { get; set; }
        public string CreatedBy { get; set; }
    }
}
