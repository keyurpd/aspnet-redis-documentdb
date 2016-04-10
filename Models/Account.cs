using Newtonsoft.Json;

namespace Keyur.AspNet.Sample
{
    public class Account
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "agency")]
        public string Agency { get; set; }

        [JsonProperty(PropertyName = "tot_emp")]
        public string Employees { get; set; }
    }
}