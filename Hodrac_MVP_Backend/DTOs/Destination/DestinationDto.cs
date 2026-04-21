using Hodrac_MVP_Backend.Enums;

namespace Hodrac_MVP_Backend.DTOs.Destination
{
    public class DestinationDto
    {
        public string DestinationName { get; set; } = string.Empty;
        public string DestinationImage { get; set; } = string.Empty;
        public DescriptionJsonDto Description { get; set; } = new DescriptionJsonDto();
        public List<TravelPeriod> BestPeriodToVisit { get; set; }
        public int MinCost { get; set; }
        public int MaxCost { get; set; }
        public int SafetyLevel { get; set; }
        public string TimeZone { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
        public List<Guid> CityIds { get; set; } = new List<Guid>();
        public List<Guid> LanguageIds { get; set; } = new List<Guid>();
        public List<Guid> CurrencyIds { get; set; } = new List<Guid>();
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
