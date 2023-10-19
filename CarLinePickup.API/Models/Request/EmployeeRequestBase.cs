using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Request
{
    public abstract class EmployeeRequestBase
    {
        public Guid EmployeeTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public bool Enabled { get; set; }
    }
}
