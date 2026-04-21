using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Tag
{
    public class TagSeedDto
    {
        [JsonPropertyName("tags")]
        public List<TagJsonDto> Tags { get; set; }
    }
}
