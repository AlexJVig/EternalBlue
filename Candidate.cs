using System.Collections.Generic;
using Newtonsoft.Json;
namespace EternalBlue
{
    public class Candidate
    {
        [JsonProperty("candidateId")]
        public string CandidateId { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public bool Gender { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("favoriteMusicGenre")]
        public string FavoriteMusicGenre { get; set; }

        [JsonProperty("dad")]
        public string Dad { get; set; }

        [JsonProperty("mom")]
        public string Mom { get; set; }

        [JsonProperty("canSwim")]
        public bool CanSwim { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("experience")]
        public List<Experience> Experience { get; set; }
    }
}
