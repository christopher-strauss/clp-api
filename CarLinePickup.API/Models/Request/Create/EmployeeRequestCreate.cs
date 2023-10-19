using CarLinePickup.API.Models.Request.Create.Interfaces;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Request.Create
{
    public class EmployeeRequestCreate : EmployeeRequestBase, IRequestCreate
    {
        public IList<EmployeeGradeRequestCreate> Grades { get; set; }
        public string CreatedBy { get; set; }
    }
}
