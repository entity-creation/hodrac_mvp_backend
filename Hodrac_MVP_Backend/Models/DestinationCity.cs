namespace Hodrac_MVP_Backend.Models
{
    public class DestinationCity
    {
        public Guid DestinationCityId { get; set; }
        public Guid DestinationId { get; set; }
        public Guid CityId { get; set; }
        public Destination Destination { get; set; }
        public City City { get; set; }
    }
}
