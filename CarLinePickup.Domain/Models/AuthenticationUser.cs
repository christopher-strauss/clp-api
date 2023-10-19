using CarLinePickup.Domain.Models.Interfaces;
using System;

namespace CarLinePickup.Domain.Models
{
    public class AuthenticationUser : ModelBase
    {
        public Guid? FamilyMemberId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string ProviderType { get; set; }
        public Guid ProviderTypeId { get; set; }
        public string AuthenticationUserType { get; set; }
        public Guid AuthenticationUserTypeId { get; set; }
        public string ExternalProviderId { get; set; }
    }
}


