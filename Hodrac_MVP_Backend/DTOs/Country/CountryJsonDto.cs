using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Country
{
    public class CountryJsonDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("continent")]
        public string Continent { get; set; }
    }
}
