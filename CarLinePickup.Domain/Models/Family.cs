using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class Family : ModelBase
    {
        public string Name { get; set; }
        public IList<FamilyMember> FamilyMembers { get; set; }
        public IList<Vehicle> Vehicles { get; set; }
        public Address Address { get; set; }
    }
}
