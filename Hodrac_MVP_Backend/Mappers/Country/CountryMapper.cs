using Hodrac_MVP_Backend.DTOs.Country;
using models = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Country
{
    public static class CountryMapper
    {
        public static models.Country FromCountryDtoToCountry(this CountryJsonDto countryDto)
        {
            return new models.Country
            {
                CountryName = countryDto.Name,
                Continent = countryDto.Continent,
            };
        }

        public static ClientCountryDto FromCountryToClientDto(this models.Country country)
        {
            return new ClientCountryDto { CountryName = country.CountryName };
        }
    }
}
