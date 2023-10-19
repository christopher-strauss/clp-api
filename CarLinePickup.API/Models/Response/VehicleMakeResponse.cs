using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class VehicleMakeResponse : ResponseBase
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
    }
}
