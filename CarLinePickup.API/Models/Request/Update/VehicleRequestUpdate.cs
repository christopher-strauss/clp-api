using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class VehicleRequestUpdate : VehicleRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
