using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class VehicleRequestCreate : VehicleRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
