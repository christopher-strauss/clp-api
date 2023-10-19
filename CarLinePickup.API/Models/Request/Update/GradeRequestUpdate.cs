using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class GradeRequestUpdate : GradeRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
