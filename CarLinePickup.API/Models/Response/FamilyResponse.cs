using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class FamilyResponse : ResponseBase
    {
        public string Name { get; set; }
        public IList<FamilyMemberResponse> FamilyMembers { get; set; }
        public IList<VehicleResponse> Vehicles { get; set; }
        public AddressResponse Address { get; set; }
    }
}
