using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class EmployeeGradeRequestUpdate : EmployeeGradeRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
