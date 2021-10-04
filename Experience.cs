using Newtonsoft.Json;
namespace EternalBlue
{
    public class Experience
    {
        [JsonProperty("technologyId")]
        public string TechnologyId { get; set; }

        [JsonProperty("yearsOfExperience")]
        public int YearsOfExperience { get; set; }
    }
}
