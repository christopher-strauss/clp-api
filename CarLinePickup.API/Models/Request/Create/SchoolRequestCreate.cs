using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class SchoolRequestCreate : SchoolRequestBase, IRequestCreate
    {
        public AddressRequestCreate Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
