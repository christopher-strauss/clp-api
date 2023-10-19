using System;
using CarLinePickup.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class VehicleModelResponse : ResponseBase
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
    }
}
