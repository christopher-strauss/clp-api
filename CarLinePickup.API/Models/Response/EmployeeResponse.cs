using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class EmployeeResponse : ResponseBase
    {
        public IList<GradeResponse> Grades { get; set; }
        public SchoolResponse School { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public string EmployeeType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
    }
}
