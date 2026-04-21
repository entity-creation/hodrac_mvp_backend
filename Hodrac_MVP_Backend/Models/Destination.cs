using Hodrac_MVP_Backend.Enums;

namespace Hodrac_MVP_Backend.Models
{
    public class Destination
    {
        public Guid DestinationId { get; set; }
        public string DestinationName { get; set; }
        public string DestinationImage { get; set; }
        public string Description { get; set; }
        public ICollection<TravelPeriod> BestPeriodToVisit { get; set; }
        public int MinCost { get; set; }
        public int MaxCost { get; set; }
        public int SafetyLevel { get; set; }
        public string TimeZone { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public ICollection<WishlistDestination> WishlistDestinations { get; set; } =
            new List<WishlistDestination>();
        public ICollection<DestinationCategory> DestinationCategories { get; set; } =
            new List<DestinationCategory>();
        public ICollection<DestinationTag> DestinationTags { get; set; } =
            new List<DestinationTag>();
        public ICollection<DestinationCurrency> DestinationCurrencies { get; set; } =
            new List<DestinationCurrency>();
        public ICollection<DestinationLanguage> DestinationLanguages { get; set; } =
            new List<DestinationLanguage>();
        public ICollection<DestinationCity> DestinationCities { get; set; } =
            new List<DestinationCity>();
    }
}
