using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class State
    {
        public State()
        {
            Addresses = new HashSet<Address>();
            Counties = new HashSet<County>();
        }

        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string StateAbbreviation { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<County> Counties { get; set; }
    }
}
