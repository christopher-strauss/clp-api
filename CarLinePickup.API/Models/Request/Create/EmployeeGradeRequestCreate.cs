using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class EmployeeGradeRequestCreate : EmployeeGradeRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
