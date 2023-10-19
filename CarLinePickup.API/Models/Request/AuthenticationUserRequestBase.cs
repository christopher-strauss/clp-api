using System;

namespace CarLinePickup.API.Models.Request
{
    public abstract class AuthenticationUserRequestBase
    {
        public string ProviderType { get; set; }
        public string ExternalProviderId { get; set; }
        public Guid AuthenticationUserTypeId { get; set; }

        public Guid? FamilyMemberId { get; set; }

        public Guid? EmployeeId { get; set; }

    }
}
