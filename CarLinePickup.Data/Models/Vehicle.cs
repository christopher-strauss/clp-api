using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Vehicle
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public Guid ModelId { get; set; }
        public string Color { get; set; }
        public string LicensePlate { get; set; }
        public Guid FamilyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual Family Family { get; set; }
        public virtual VehicleMake Make { get; set; }
        public virtual VehicleModel Model { get; set; }
    }
}
