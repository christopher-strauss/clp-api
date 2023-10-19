using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class VehicleMake
    {
        public VehicleMake()
        {
            VehicleModels = new HashSet<VehicleModel>();
            Vehicles = new HashSet<Vehicle>();
        }

        public Guid Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
