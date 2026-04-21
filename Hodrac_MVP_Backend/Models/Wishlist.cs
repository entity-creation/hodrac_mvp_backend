namespace Hodrac_MVP_Backend.Models
{
    public class Wishlist
    {
        public Guid WishlistId { get; set; }
        public string WishlistName { get; set; }
        public string WishlistDescription { get; set; }
        public string ShortStory { get; set; }
        public int TotalDays { get; set; }
        public string PeopleType { get; set; }
        public string WishlistHeroImage { get; set; }
        public ICollection<ItineraryDay> ItineraryDays { get; set; } = new List<ItineraryDay>();
        public ICollection<WishlistDestination> WishlistDestinations { get; set; } =
            new List<WishlistDestination>();
    }
}
