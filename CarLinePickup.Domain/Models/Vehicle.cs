using System;

namespace CarLinePickup.Domain.Models
{
    public class Vehicle : ModelBase
    {
        public Family Family { get; set; }
        public Guid MakeId { get; set; }
        public string Make { get; set; }
        public Guid ModelId { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
    }
}
