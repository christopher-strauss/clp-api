using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class CountyRequestUpdate : CountyRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
