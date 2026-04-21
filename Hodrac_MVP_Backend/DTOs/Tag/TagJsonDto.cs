using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Tag
{
    public class TagJsonDto
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
