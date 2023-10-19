using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class VehicleMake : ModelBase
    {
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public IList<VehicleModel> Models { get; set; }
    }
}
