using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class State : ModelBase
    {
        public IList<County> Counties { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
