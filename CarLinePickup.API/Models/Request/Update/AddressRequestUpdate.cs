using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class AddressRequestUpdate : AddressRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
