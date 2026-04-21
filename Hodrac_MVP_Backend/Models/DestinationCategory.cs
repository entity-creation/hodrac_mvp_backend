namespace Hodrac_MVP_Backend.Models
{
    public class DestinationCategory
    {
        public Guid DestinationCategoryId { get; set; }
        public Destination Destination { get; set; }
        public Category Category { get; set; }
        public Guid DestinationId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
