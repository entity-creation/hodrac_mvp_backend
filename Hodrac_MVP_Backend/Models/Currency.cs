namespace Hodrac_MVP_Backend.Models
{
    public class Currency
    {
        public Guid CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public ICollection<DestinationCurrency> DestinationCurrencies { get; set; } =
            new List<DestinationCurrency>();
    }
}
