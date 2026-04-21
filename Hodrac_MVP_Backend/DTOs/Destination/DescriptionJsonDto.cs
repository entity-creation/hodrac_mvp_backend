using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Destination
{
    public class DescriptionJsonDto
    {
        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("localPerspective")]
        public string LocalPerspective { get; set; } = string.Empty;

        [JsonPropertyName("directions")]
        public string Directions { get; set; } = string.Empty;

        [JsonPropertyName("whatToKnow")]
        public string WhatToKnow { get; set; } = string.Empty;

        [JsonPropertyName("thingsToBeWaryOf")]
        public string ThingsToBeWaryOf { get; set; } = string.Empty;

        [JsonPropertyName("hiddenCost")]
        public string HiddenCost { get; set; } = string.Empty;

        [JsonPropertyName("nearbyComplements")]
        public List<string> NearbyComplements { get; set; } = new List<string>();

        [JsonPropertyName("bestTimeToVisit")]
        public string BestTimeToVisit { get; set; } = string.Empty;

        [JsonPropertyName("crowdLevel")]
        public string crowdLevel { get; set; } = string.Empty;

        [JsonPropertyName("accessibility")]
        public string Accessibility { get; set; } = string.Empty;

        [JsonPropertyName("idealDuration")]
        public string IdealDuration { get; set; } = string.Empty;
    }
}
