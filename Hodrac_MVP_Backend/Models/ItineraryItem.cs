namespace Hodrac_MVP_Backend.Models
{
    public class ItineraryItem
    {
        public Guid ItineraryItemId { get; set; }
        public string ItemDescription { get; set; }
        public int ItemOrderIndex { get; set; }
        public string TimeOfDay { get; set; }
        public string ItemType { get; set; }
        public ItineraryDay ItineraryDay { get; set; }
        public Guid ItineraryDayId { get; set; }
    }
}
