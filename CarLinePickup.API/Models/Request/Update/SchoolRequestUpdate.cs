using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class SchoolRequestUpdate : SchoolRequestBase, IRequestUpdate
    {
        public AddressRequestUpdate Address { get; set; }
        public string ModifiedBy { get; set; }
    }
}
