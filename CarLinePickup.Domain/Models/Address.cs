using CarLinePickup.Domain.Models.Interfaces;
using System;

namespace CarLinePickup.Domain.Models
{
    public class Address : ModelBase
    {
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public Guid StateId { get; set; }
        public Guid CountyId { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
    }
}
