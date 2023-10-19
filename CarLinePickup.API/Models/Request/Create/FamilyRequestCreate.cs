using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class FamilyRequestCreate : FamilyRequestBase, IRequestCreate
    {
        public AddressRequestCreate Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
