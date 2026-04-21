namespace Hodrac_MVP_Backend.Models
{
    public class DestinationCurrency
    {
        public Guid DestinationCurrencyId { get; set; }
        public Destination Destination { get; set; }
        public Currency Currency { get; set; }
        public Guid DestinationId { get; set; }
        public Guid CurrencyId { get; set; }
    }
}
