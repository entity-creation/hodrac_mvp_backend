namespace Hodrac_MVP_Backend.Models
{
    public class WishlistDestination
    {
        public Guid WishlistDestinationId { get; set; }
        public Guid DestinationId { get; set; }
        public Guid WishlistId { get; set; }
        public Destination Destination { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
