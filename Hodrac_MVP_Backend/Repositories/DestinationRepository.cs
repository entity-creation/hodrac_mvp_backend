using System.Text.Json;
using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.Destination;
using Hodrac_MVP_Backend.Enums;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Destination;
using Microsoft.EntityFrameworkCore;

namespace Hodrac_MVP_Backend.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly CustomDbContext _context;

        public DestinationRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewDestination(DestinationDto destinationDto)
        {
            var destination = destinationDto.FromDtoToDestination();
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClientDestinationDto>> GetAllDestinations()
        {
            var destinationQuery = _context.Destinations.AsQueryable();

            List<ClientDestinationDto> destinationList = await destinationQuery
                .Select(d => new ClientDestinationDto
                {
                    DestinationId = d.DestinationId,
                    DestinationName = d.DestinationName,
                    DestinationImage = d.DestinationImage,
                    Description = d.Description,
                    BestPeriodToVisit = d.BestPeriodToVisit.ToList(),
                    CostRange = $"{d.MinCost}-{d.MaxCost}",
                    SafetyLevel = d.SafetyLevel,
                    TimeZone = d.TimeZone,
                    CountryName = d.Country.CountryName,
                    Tags = d.DestinationTags.Select(dt => dt.Tag.TagName).ToList(),
                    Categories = d
                        .DestinationCategories.Select(dc => dc.Category.CategoryName)
                        .ToList(),
                    Cities = d.DestinationCities.Select(dc => dc.City.CityName).ToList(),
                    Languages = d
                        .DestinationLanguages.Select(dl => dl.Language.LanguageName)
                        .ToList(),
                    Currencies = d
                        .DestinationCurrencies.Select(dl => dl.Currency.CurrencyName)
                        .ToList(),
                })
                .ToListAsync();

            return destinationList;
        }

        public async Task<ClientDestinationDto?> GetDestinationById(Guid destinationId)
        {
            var destination = await _context
                .Destinations.AsNoTracking()
                .Where(d => d.DestinationId == destinationId)
                .Select(destination => new ClientDestinationDto
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
                        .DestinationCurrencies.Select(dl => dl.Currency.CurrencyName)
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            return destination;
        }

        public async Task<List<ClientDestinationDto>> GetDestinationByQuery(
            DestinationQueryDto query
        )
        {
            TravelPeriod? travelPeriod = null;
            if (
                !string.IsNullOrEmpty(query.BestPeriodToVisit)
                && Enum.TryParse<TravelPeriod>(query.BestPeriodToVisit, out var parsed)
            )
            {
                travelPeriod = parsed;
            }

            var destinations = _context
                .Destinations.Include(d => d.DestinationCategories)
                    .ThenInclude(c => c.Category)
                .Include(d => d.DestinationTags)
                    .ThenInclude(c => c.Tag)
                .Include(d => d.DestinationCurrencies)
                    .ThenInclude(c => c.Currency)
                .Include(d => d.DestinationLanguages)
                    .ThenInclude(l => l.Language)
                .Include(d => d.DestinationCities)
                    .ThenInclude(c => c.City)
                .Include(c => c.Country)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Country))
                destinations = destinations.Where(d => d.Country.CountryName.Equals(query.Country));

            if (query.SafetyLevels != null && query.SafetyLevels.Count == 2)
                destinations = destinations.Where(d =>
                    d.SafetyLevel >= query.SafetyLevels.ElementAt(0)
                    && d.SafetyLevel <= query.SafetyLevels.ElementAt(1)
                );

            if (!string.IsNullOrEmpty(query.Categories))
                destinations = destinations.Where(d =>
                    d.DestinationCategories.Any(dc => dc.Category.CategoryName == query.Categories)
                );

            if (!string.IsNullOrEmpty(query.Tags))
                destinations = destinations.Where(d =>
                    d.DestinationTags.Any(dc => dc.Tag.TagName == query.Tags)
                );

            var result = await destinations.ToListAsync();

            if (travelPeriod.HasValue)
                result = result
                    .Where(d => d.BestPeriodToVisit.Contains(travelPeriod.Value))
                    .ToList();
            return destinations
                .Select(d => new ClientDestinationDto
                {
                    DestinationId = d.DestinationId,
                    DestinationName = d.DestinationName,
                    DestinationImage = d.DestinationImage,
                    Description = d.Description,
                    BestPeriodToVisit = d.BestPeriodToVisit.ToList(),
                    CostRange = $"{d.MinCost}-{d.MaxCost}",
                    SafetyLevel = d.SafetyLevel,
                    TimeZone = d.TimeZone,
                    CountryName = d.Country.CountryName,
                    Tags = d.DestinationTags.Select(dt => dt.Tag.TagName).ToList(),
                    Categories = d
                        .DestinationCategories.Select(dc => dc.Category.CategoryName)
                        .ToList(),
                    Cities = d.DestinationCities.Select(dc => dc.City.CityName).ToList(),
                    Languages = d
                        .DestinationLanguages.Select(dl => dl.Language.LanguageName)
                        .ToList(),
                    Currencies = d
                        .DestinationCurrencies.Select(dl => dl.Currency.CurrencyName)
                        .ToList(),
                })
                .ToList();
        }
    }
}
