namespace Hodrac_MVP_Backend.Models
{
    public class Country
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public string Continent { get; set; }
        public ICollection<Destination> Destinations { get; set; } = new List<Destination>();
        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
