using System;

namespace CarLinePickup.API.Models.Request
{
    public abstract class VehicleRequestBase
    {
        public string Year { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
    }
}
