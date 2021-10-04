using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace EternalBlue
{
    public class Services
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<List<Candidate>> GetCandidates()
        {
            var task = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/candidates");
            var jsonString = await task.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Candidate>>(jsonString);
        }

        public async Task<List<Technology>> GetTechnologies()
        {
            var task = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/technologies");
            var jsonString = await task.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Technology>>(jsonString);
        }
    }
}
