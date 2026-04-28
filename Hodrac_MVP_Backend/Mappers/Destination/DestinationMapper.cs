using System.Text.Json;
using Hodrac_MVP_Backend.DTOs.Destination;
using models = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Destination
{
    public static class DestinationMapper
    {
        public static models.Destination FromDtoToDestination(this DestinationDto dto)
        {
            return new models.Destination()
            {
                DestinationName = dto.DestinationName,
                DestinationImage = dto.DestinationImage,
                BestPeriodToVisit = dto.BestPeriodToVisit,
                MinCost = dto.MinCost,
                MaxCost = dto.MaxCost,
                SafetyLevel = dto.SafetyLevel,
                TimeZone = dto.TimeZone,
                Description = dto.Description.ConvertDescriptionToJson(),
                CountryId = dto.CountryId,
                DestinationCategories = dto
                    .CategoryIds.Select(categoryId => new models.DestinationCategory()
                    {
                        CategoryId = categoryId,
                    })
                    .ToList(),
                DestinationTags = dto
                    .TagIds.Select(tagId => new models.DestinationTag() { TagId = tagId })
                    .ToList(),
                DestinationLanguages = dto
                    .LanguageIds.Select(languageId => new models.DestinationLanguage()
                    {
                        LanguageId = languageId,
                    })
                    .ToList(),
                DestinationCurrencies = dto
                    .CurrencyIds.Select(currencyId => new models.DestinationCurrency()
                    {
                        CurrencyId = currencyId,
                    })
                    .ToList(),
                DestinationCities = dto
                    .CityIds.Select(cityId => new models.DestinationCity() { CityId = cityId })
                    .ToList(),
            };
        }

        public static ClientDestinationDto FromDestinationToClientDto(
            this models.Destination destination
        )
        {
            return new ClientDestinationDto
            {
                DestinationId = destination.DestinationId,
                DestinationName = destination.DestinationName,
                DestinationImage = destination.DestinationImage,
                Description = destination.Description,
                BestPeriodToVisit = destination.BestPeriodToVisit.ToList(),
                CostRange = $"{destination.MinCost}-{destination.MaxCost}",
                SafetyLevel = destination.SafetyLevel,
                TimeZone = destination.TimeZone,
                CountryName = destination.Country.CountryName,
                Tags = destination.DestinationTags.Select(dt => dt.Tag.TagName).ToList(),
                Categories = destination
                    .DestinationCategories.Select(dc => dc.Category.CategoryName)
                    .ToList(),
                Cities = destination.DestinationCities.Select(dc => dc.City.CityName).ToList(),
                Languages = destination
                    .DestinationLanguages.Select(dl => dl.Language.LanguageName)
                    .ToList(),
                Currencies = destination
                    .DestinationCurrencies.Select(dc => dc.Currency.CurrencyName)
                    .ToList(),
            };
        }

        public static string ConvertDescriptionToJson(this DescriptionJsonDto dto)
        {
            var jsonResult = JsonSerializer.Serialize(dto);
            if (jsonResult == null)
                return string.Empty;
            return jsonResult;
        }
    }
}
