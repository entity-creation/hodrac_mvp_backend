using Hodrac_MVP_Backend.DTOs.Currency;
using model = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Currency
{
    public static class CurrencyMapper
    {
        public static model.Currency FromCurrencyJsonToCurrency(
            this CurrencyJsonDto currencyJsonDto
        )
        {
            return new model.Currency
            {
                CurrencyName = currencyJsonDto.Name,
                CurrencySymbol = currencyJsonDto.Code,
            };
        }
    }
}
