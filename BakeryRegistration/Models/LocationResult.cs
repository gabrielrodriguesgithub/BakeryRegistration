using Newtonsoft.Json;

namespace BakeryRegistration.Models
{
    public class LocationResult
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }
    }
}
