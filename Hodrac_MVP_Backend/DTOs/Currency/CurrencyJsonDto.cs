using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Currency
{
    public class CurrencyJsonDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
