namespace Hodrac_MVP_Backend.DTOs.ItineraryItem
{
    public class ClientItineraryItemDto
    {
        public string ItemDescription { get; set; } = string.Empty;
        public int ItemOrderIndex { get; set; }
        public int DayNumber { get; set; }
        public string TimeOfDay { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
    }
}
