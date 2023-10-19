using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class FamilyMemberRequestUpdate : FamilyMemberRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
