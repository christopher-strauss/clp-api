using System;

namespace CarLinePickup.API.Models.Response
{
    public class StateResponse : ResponseBase
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
