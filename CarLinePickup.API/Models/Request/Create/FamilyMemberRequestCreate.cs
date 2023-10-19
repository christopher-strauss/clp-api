using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class FamilyMemberRequestCreate : FamilyMemberRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
