using System;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models
{
    public class Employee : ModelBase
    {
        public AuthenticationUser AuthenticationUser { get; set; }
        public IList<Grade> Grades { get; set; }
        public School School { get; set; }
        public IList<FamilyMember> FamilyMembers { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public string EmployeeType { get; set; }
        public bool Enabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
