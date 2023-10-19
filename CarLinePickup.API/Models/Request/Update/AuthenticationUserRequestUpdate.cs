using CarLinePickup.API.Models.Request.Update.Interfaces;

namespace CarLinePickup.API.Models.Request.Update
{
    public class AuthenticationUserRequestUpdate : AuthenticationUserRequestBase, IRequestUpdate
    {
        public string ModifiedBy { get; set; }
    }
}
