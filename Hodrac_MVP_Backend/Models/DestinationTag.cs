namespace Hodrac_MVP_Backend.Models
{
    public class DestinationTag
    {
        public Guid DestinationTagId { get; set; }
        public Destination Destination { get; set; }
        public Tag Tag { get; set; }
        public Guid DestinationId { get; set; }
        public Guid TagId { get; set; }
    }
}
