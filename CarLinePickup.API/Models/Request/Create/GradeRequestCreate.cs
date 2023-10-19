using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class GradeRequestCreate : GradeRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
