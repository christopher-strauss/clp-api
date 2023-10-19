using CarLinePickup.API.Models.Request.Create.Interfaces;

namespace CarLinePickup.API.Models.Request.Create
{
    public class AuthenticationUserRequestCreate : AuthenticationUserRequestBase, IRequestCreate
    {
        public string CreatedBy { get; set; }
    }
}
