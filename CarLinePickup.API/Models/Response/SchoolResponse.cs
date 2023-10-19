using System;
using System.Collections.Generic;

namespace CarLinePickup.API.Models.Response
{
    public class SchoolResponse : ResponseBase
    {
        public string Name { get; set; }
        public AddressResponse Address { get; set; }
        public IList<GradeResponse> Grades { get; set; }
    }
}
