using CarLinePickup.Domain.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarLinePickup.Domain.Models.VPIC
{
    public class ModelResult
    {
        [JsonProperty(PropertyName = "Results")]
        public IList<Model> Models { get; set; }
    }

    public class Model
    {
        [JsonProperty(PropertyName = "Model_ID")] 
        public int Id { get; set; }
        [JsonProperty(PropertyName = "Model_Name")] 
        public string Name { get; set; }
    }
}
