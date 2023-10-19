using CarLinePickup.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models.VPIC
{
    public class MakeResult
    {
        [JsonProperty(PropertyName = "Results")]
        public IList<Make> Makes { get; set; }
    }

    public class Make
    {
        [JsonProperty(PropertyName = "MakeId")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "MakeName")]
        public string Name { get; set; }
    }
}
