using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public int ExternalId { get; set; }
        public Guid VehicleMakeId { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
