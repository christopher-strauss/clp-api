using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class StateRequestCreate : StateRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
