using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Destination
{
    public class DestinationQueryDto
    {
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("bestPeriodToVisit")]
        public string? BestPeriodToVisit { get; set; }

        [JsonPropertyName("safetyLevels")]
        public ICollection<int>? SafetyLevels { get; set; }

        [JsonPropertyName("categories")]
        public string? Categories { get; set; }

        [JsonPropertyName("tags")]
        public string? Tags { get; set; }

        [JsonPropertyName("priceRange")]
        public ICollection<int>? PriceRange { get; set; }
    }
}
