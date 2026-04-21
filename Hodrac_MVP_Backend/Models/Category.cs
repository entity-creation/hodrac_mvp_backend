namespace Hodrac_MVP_Backend.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Key { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public ICollection<DestinationCategory> DestinationCategories { get; set; } =
            new List<DestinationCategory>();
    }
}
