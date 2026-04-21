namespace Hodrac_MVP_Backend.Models
{
    public class ItineraryDay
    {
        public Guid ItineraryDayId { get; set; }
        public int DayNumber { get; set; }
        public string DayTitle { get; set; }
        public Wishlist Wishlist { get; set; }
        public Guid WishlistId { get; set; }
        public ICollection<ItineraryItem> ItineraryItems { get; set; } = new List<ItineraryItem>();
    }
}
