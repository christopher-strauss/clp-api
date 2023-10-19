using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            Families = new HashSet<Family>();
            Schools = new HashSet<School>();
        }

        public Guid Id { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public Guid StateId { get; set; }
        public Guid CountyId { get; set; }
        public string Zip { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual County County { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<Family> Families { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}
