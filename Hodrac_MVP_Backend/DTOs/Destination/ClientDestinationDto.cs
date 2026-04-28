using Hodrac_MVP_Backend.Enums;

namespace Hodrac_MVP_Backend.DTOs.Destination
{
    public class ClientDestinationDto
    {
        public Guid DestinationId { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public string DestinationImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<TravelPeriod> BestPeriodToVisit { get; set; } = new List<TravelPeriod>();
        public string CostRange { get; set; } = string.Empty;
        public int SafetyLevel { get; set; }
        public string TimeZone { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Languages { get; set; } = new List<string>();
        public List<string> Currencies { get; set; } = new List<string>();
        public List<string> Cities { get; set; } = new List<string>();
    }
}
