using Newtonsoft.Json;
namespace EternalBlue
{
    public class Technology
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }
    }
}
