using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.Destination;
using Hodrac_MVP_Backend.DTOs.ItineraryDay;
using Hodrac_MVP_Backend.DTOs.ItineraryItem;
using Hodrac_MVP_Backend.DTOs.Wishlist;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Wishlist;
using Hodrac_MVP_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Hodrac_MVP_Backend.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly CustomDbContext _context;

        public WishlistRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewWishlist(QueryWishlistDto dto, List<Guid> destinationIds)
        {
            var wishlist = dto.FromQueryDtoToWishlist();
            var destinations = await _context
                .Destinations.Where(d => destinationIds.Contains(d.DestinationId))
                .ToListAsync();

            if (destinations.Count != destinationIds.Count)
                throw new Exception("Some destinations not found");
            wishlist.WishlistDestinations = destinations
                .Select(d => new WishlistDestination
                {
                    DestinationId = d.DestinationId,
                    WishlistId = wishlist.WishlistId,
                })
                .ToList();
            await _context.Wishlists.AddAsync(wishlist);
            await _context.SaveChangesAsync();
            //var createdWishlist = await GetWishlistById(wishlist.WishlistId);
            //return createdWishlist;
        }

        public async Task<List<ClientWishlistDto>> GetAllWishlist()
        {
            return await _context
                .Wishlists.AsNoTracking()
                .Select(w => new ClientWishlistDto
                {
                    WishlistId = w.WishlistId,
                    WishlistDescription = w.WishlistDescription,
                    WishlistName = w.WishlistName,
                    ShortStory = w.ShortStory,
                    TotalDays = w.TotalDays,
                    WishlistHeroImage = w.WishlistHeroImage,
                    PeopleType = w.PeopleType,

                    ItineraryDays = w
                        .ItineraryDays.Select(d => new ClientItineraryDayDto
                        {
                            DayNumber = d.DayNumber,
                            DayTitle = d.DayTitle,
                        })
                        .ToList(),

                    ItineraryItems = w
                        .ItineraryDays.SelectMany(d => d.ItineraryItems)
                        .Select(i => new ClientItineraryItemDto
                        {
                            DayNumber = i.ItineraryDay.DayNumber,
                            ItemDescription = i.ItemDescription,
                            ItemOrderIndex = i.ItemOrderIndex,
                            ItemType = i.ItemType,
                            TimeOfDay = i.TimeOfDay,
                        })
                        .ToList(),

                    Destinations = w
                        .WishlistDestinations.Select(wd => new ClientDestinationDto
                        {
                            DestinationId = wd.DestinationId,
                            DestinationName = wd.Destination.DestinationName,
                            DestinationImage = wd.Destination.DestinationImage,
                            Description = wd.Destination.Description,

                            CountryName = wd.Destination.Country.CountryName,

                            Tags = wd
                                .Destination.DestinationTags.Select(t => t.Tag.TagName)
                                .ToList(),

                            Categories = wd
                                .Destination.DestinationCategories.Select(c =>
                                    c.Category.CategoryName
                                )
                                .ToList(),

                            Cities = wd
                                .Destination.DestinationCities.Select(c => c.City.CityName)
                                .ToList(),

                            Languages = wd
                                .Destination.DestinationLanguages.Select(l =>
                                    l.Language.LanguageName
                                )
                                .ToList(),

                            Currencies = wd
                                .Destination.DestinationCurrencies.Select(c =>
                                    c.Currency.CurrencyName
                                )
                                .ToList(),
                        })
                        .ToList(),
                })
                .ToListAsync();
        }

        public async Task<ClientWishlistDto> GetWishlistById(Guid id)
        {
            var wishlist = await _context
                .Wishlists.AsNoTracking()
                .Where(w => w.WishlistId == id)
                .Select(w => new ClientWishlistDto
                {
                    WishlistId = w.WishlistId,
                    WishlistDescription = w.WishlistDescription,
                    WishlistName = w.WishlistName,
                    ShortStory = w.ShortStory,
                    TotalDays = w.TotalDays,
                    WishlistHeroImage = w.WishlistHeroImage,
                    PeopleType = w.PeopleType,

                    ItineraryDays = w
                        .ItineraryDays.Select(d => new ClientItineraryDayDto
                        {
                            DayNumber = d.DayNumber,
                            DayTitle = d.DayTitle,
                        })
                        .ToList(),

                    ItineraryItems = w
                        .ItineraryDays.SelectMany(d => d.ItineraryItems)
                        .Select(i => new ClientItineraryItemDto
                        {
                            DayNumber = i.ItineraryDay.DayNumber,
                            ItemDescription = i.ItemDescription,
                            ItemOrderIndex = i.ItemOrderIndex,
                            ItemType = i.ItemType,
                            TimeOfDay = i.TimeOfDay,
                        })
                        .ToList(),

                    Destinations = w
                        .WishlistDestinations.Select(wd => new ClientDestinationDto
                        {
                            DestinationId = wd.DestinationId,
                            DestinationName = wd.Destination.DestinationName,
                            DestinationImage = wd.Destination.DestinationImage,
                            Description = wd.Destination.Description,
                            SafetyLevel = wd.Destination.SafetyLevel,
                            TimeZone = wd.Destination.TimeZone,

                            CountryName = wd.Destination.Country.CountryName,

                            Tags = wd
                                .Destination.DestinationTags.Select(t => t.Tag.TagName)
                                .ToList(),

                            Categories = wd
                                .Destination.DestinationCategories.Select(c =>
                                    c.Category.CategoryName
                                )
                                .ToList(),

                            Cities = wd
                                .Destination.DestinationCities.Select(c => c.City.CityName)
                                .ToList(),

                            Languages = wd
                                .Destination.DestinationLanguages.Select(l =>
                                    l.Language.LanguageName
                                )
                                .ToList(),

                            Currencies = wd
                                .Destination.DestinationCurrencies.Select(c =>
                                    c.Currency.CurrencyName
                                )
                                .ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefaultAsync();

            return wishlist ?? new ClientWishlistDto();
        }
    }
}
