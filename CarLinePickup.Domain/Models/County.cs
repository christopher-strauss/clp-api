using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class County : ModelBase
    {
        public State State { get; set; }
        public string Name { get; set; }
    }
}
