namespace Hodrac_MVP_Backend.Models
{
    public class Language
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; }
        public ICollection<DestinationLanguage> DestinationLanguages { get; set; } =
            new List<DestinationLanguage>();
    }
}
