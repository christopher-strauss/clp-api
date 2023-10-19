using CarLinePickup.API.Models.Request.Create.Interfaces;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Request.Create
{
    public class FamilyRegistrationRequestCreate : FamilyRequestBase, IRequestCreate
    {
        public IList<FamilyMemberRegistrationRequestCreate> FamilyMembers { get; set; }
        public IList<VehicleRequestCreate> Vehicles { get; set; }
        public AddressRequestCreate Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
