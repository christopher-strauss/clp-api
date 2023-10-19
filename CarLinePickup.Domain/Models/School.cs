using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class School : ModelBase
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public IList<Grade> Grades { get; set; }
    }
}
