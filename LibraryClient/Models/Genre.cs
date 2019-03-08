using Newtonsoft.Json;

namespace LibraryClient.Models
{
    public class Genre
    {
        [JsonProperty(PropertyName = "genre")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "full_title")]
        public string Name { get; set; }
    }
}