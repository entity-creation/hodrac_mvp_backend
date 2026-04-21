namespace Hodrac_MVP_Backend.Models
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Key { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public ICollection<DestinationTag> DestinationTags { get; set; } =
            new List<DestinationTag>();
    }
}
