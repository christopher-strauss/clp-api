using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class County
    {
        public County()
        {
            Addresses = new HashSet<Address>();
        }

        public Guid Id { get; set; }
        public Guid StateId { get; set; }
        public string CountyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
