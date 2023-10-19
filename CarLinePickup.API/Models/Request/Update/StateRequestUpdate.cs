using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class StateRequestUpdate : StateRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
