namespace Hodrac_MVP_Backend.Models
{
    public class DestinationLanguage
    {
        public Guid DestinationLanguageId { get; set; }
        public Destination Destination { get; set; }
        public Language Language { get; set; }
        public Guid LanguageId { get; set; }
        public Guid DestinationId { get; set; }
    }
}
