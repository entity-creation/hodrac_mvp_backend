using System.Text.Json.Serialization;

namespace Hodrac_MVP_Backend.DTOs.Category
{
    public class CategorySeedDto
    {
        [JsonPropertyName("categories")]
        public List<CategoryJsonDto> Categories { get; set; }
    }
}
