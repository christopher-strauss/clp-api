using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class CountyResponse : ResponseBase
    {
        public IList<StateResponse> States { get; set; }
        public string Name { get; set; }
    }
}
