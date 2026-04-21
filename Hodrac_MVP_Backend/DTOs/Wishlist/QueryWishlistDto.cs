using System.Text.Json.Serialization;
using Hodrac_MVP_Backend.DTOs.ItineraryDay;
using Hodrac_MVP_Backend.DTOs.ItineraryItem;

namespace Hodrac_MVP_Backend.DTOs.Wishlist
{
    public class QueryWishlistDto
    {
        [JsonPropertyName("wishlistId")]
        public Guid WishlistId { get; set; } = Guid.Empty;

        [JsonPropertyName("wishlistName")]
        public string WishlistName { get; set; } = string.Empty;

        [JsonPropertyName("wishlistDescription")]
        public string WishlistDescription { get; set; } = string.Empty;

        [JsonPropertyName("wishlistHeroImage")]
        public string WishlistHeroImage { get; set; } = string.Empty;

        [JsonPropertyName("shortStory")]
        public string ShortStory { get; set; } = string.Empty;

        [JsonPropertyName("totalDays")]
        public int TotalDays { get; set; }

        [JsonPropertyName("peopleType")]
        public string PeopleType { get; set; } = string.Empty;

        [JsonPropertyName("itineraryDays")]
        public List<ClientItineraryDayDto> ItineraryDays { get; set; } =
            new List<ClientItineraryDayDto>();

        [JsonPropertyName("itineraryItems")]
        public List<ClientItineraryItemDto> ItineraryItems { get; set; } =
            new List<ClientItineraryItemDto>();
    }
}
