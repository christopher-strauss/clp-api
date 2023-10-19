using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class CountyRequestCreate : CountyRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
