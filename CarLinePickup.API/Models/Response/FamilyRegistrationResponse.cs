using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class FamilyRegistrationResponse : ResponseBase
    {
        AuthenticationUserResponse AuthenticationUser { get; set; }
        public FamilyResponse Family { get; set; }
    }
}
