namespace Hodrac_MVP_Backend.Models
{
    public class City
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<DestinationCity> DestinationCities { get; set; } =
            new List<DestinationCity>();
    }
}
