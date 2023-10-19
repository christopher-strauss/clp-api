using System;

namespace CarLinePickup.API.Models.Response
{
    public class AddressResponse : ResponseBase
    {
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public Guid StateId { get; set; }
        public Guid CountyId { get; set; }
        public string Zip { get; set; }
    }
}
