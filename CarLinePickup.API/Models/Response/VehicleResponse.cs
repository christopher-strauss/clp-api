using System;

namespace CarLinePickup.API.Models.Response
{
    public class VehicleResponse : ResponseBase
    {
        public string Year { get; set; }
        public Guid MakeId { get; set; }
        public string Make { get; set; }
        public Guid ModelId { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
    }
}
