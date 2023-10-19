using System;
using System.Collections.Generic;

#nullable disable

namespace CarLinePickup.Data.Models
{
    public partial class Grade
    {
        public Grade()
        {
            EmployeeGrades = new HashSet<EmployeeGrade>();
        }

        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<EmployeeGrade> EmployeeGrades { get; set; }
    }
}
