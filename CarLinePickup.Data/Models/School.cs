using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class School
    {
        public School()
        {
            Employees = new HashSet<Employee>();
            Grades = new HashSet<Grade>();
        }

        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public Guid AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
