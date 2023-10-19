using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class AddressRequestCreate : AddressRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
