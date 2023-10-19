using System;


namespace CarLinePickup.API.Models.Response
{
    public class AuthenticationUserResponse : ResponseBase
    {
        public Guid? FamilyMemberId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid ProviderTypeId { get; set; }
        public string ProviderType { get; set; }
        public Guid AuthenticationUserTypeId { get; set; }
        public string AuthenticationUserType { get; set; }
        public string ExternalProviderId { get; set; }
    }
}
