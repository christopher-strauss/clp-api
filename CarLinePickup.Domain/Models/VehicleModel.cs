using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class VehicleModel : ModelBase
    {
        public int ExternalId { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
    }
}
