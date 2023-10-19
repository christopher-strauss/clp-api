using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeGrades = new HashSet<EmployeeGrade>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public Guid SchoolId { get; set; }
        public Guid EmployeeTypeId { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }
        public virtual School School { get; set; }
        public virtual AuthenticationUser AuthenticationUser { get; set; }
        public virtual ICollection<EmployeeGrade> EmployeeGrades { get; set; }
    }
}
