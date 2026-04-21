using Hodrac_MVP_Backend.DTOs.Destination;
using Hodrac_MVP_Backend.DTOs.ItineraryDay;
using Hodrac_MVP_Backend.DTOs.ItineraryItem;

namespace Hodrac_MVP_Backend.DTOs.Wishlist
{
    public class ClientWishlistDto
    {
        public Guid WishlistId { get; set; } = Guid.Empty;
        public string WishlistName { get; set; } = string.Empty;
        public string WishlistDescription { get; set; } = string.Empty;
        public string WishlistHeroImage { get; set; } = string.Empty;
        public string ShortStory { get; set; } = string.Empty;
        public int TotalDays { get; set; }
        public string PeopleType { get; set; } = string.Empty;
        public List<ClientItineraryDayDto> ItineraryDays { get; set; } =
            new List<ClientItineraryDayDto>();
        public List<ClientItineraryItemDto> ItineraryItems { get; set; } =
            new List<ClientItineraryItemDto>();
        public List<ClientDestinationDto> Destinations { get; set; } =
            new List<ClientDestinationDto>();
    }
}
