using System;

namespace CarLinePickup.API.Models.Request
{
    public abstract class AddressRequestBase
    {
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public Guid StateId { get; set; }
        public Guid CountyId { get; set; }
        public string Zip { get; set; }
    }
}
