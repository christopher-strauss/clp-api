using System;

namespace CarLinePickup.API.Models.Request
{
    public abstract class EmployeeGradeRequestBase
    {
        public Guid GradeId { get; set; }
        public Guid EmployeeId { get; set; }

    }
}
