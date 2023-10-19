using CarLinePickup.API.Models.Request.Update.Interfaces;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Request.Update
{
    public class EmployeeRequestUpdate : EmployeeRequestBase, IRequestUpdate
    {
        public IList<EmployeeGradeRequestUpdate> Grades { get; set; }
        public string ModifiedBy { get; set; }
    }
}
