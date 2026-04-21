using Hodrac_MVP_Backend.DTOs.ItineraryDay;
using Hodrac_MVP_Backend.DTOs.ItineraryItem;
using Hodrac_MVP_Backend.DTOs.Wishlist;
using Hodrac_MVP_Backend.Mappers.Destination;
using Hodrac_MVP_Backend.Models;
using models = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Wishlist
{
    public static class WishlistMapper
    {
        public static ClientWishlistDto FromWishlistToDto(this models.Wishlist wishlist)
        {
            return new ClientWishlistDto
            {
                WishlistId = wishlist.WishlistId,
                WishlistDescription = wishlist.WishlistDescription,
                WishlistName = wishlist.WishlistName,
                ShortStory = wishlist.ShortStory,
                TotalDays = wishlist.TotalDays,
                WishlistHeroImage = wishlist.WishlistHeroImage,
                PeopleType = wishlist.PeopleType,
                ItineraryDays = wishlist
                    .ItineraryDays.Select(i => new ClientItineraryDayDto
                    {
                        DayNumber = i.DayNumber,
                        DayTitle = i.DayTitle,
                    })
                    .ToList(),
                ItineraryItems = wishlist
                    .ItineraryDays.SelectMany(day =>
                        day.ItineraryItems.Select(item => new { day, item })
                    )
                    .Select(x => new ClientItineraryItemDto
                    {
                        DayNumber = x.day.DayNumber,
                        ItemDescription = x.item.ItemDescription,
                        ItemOrderIndex = x.item.ItemOrderIndex,
                        ItemType = x.item.ItemType,
                        TimeOfDay = x.item.TimeOfDay,
                    })
                    .ToList(),
                Destinations = wishlist
                    .WishlistDestinations.Select(d => d.Destination.FromDestinationToClientDto())
                    .ToList(),
            };
        }

        public static models.Wishlist FromQueryDtoToWishlist(this QueryWishlistDto query)
        {
            return new models.Wishlist
            {
                WishlistId = query.WishlistId,
                WishlistDescription = query.WishlistDescription,
                WishlistName = query.WishlistName,
                PeopleType = query.PeopleType,
                ShortStory = query.ShortStory,
                TotalDays = query.TotalDays,
                WishlistHeroImage = query.WishlistHeroImage,
                ItineraryDays = query
                    .ItineraryDays.Select(day =>
                    {
                        var dayId = Guid.NewGuid();
                        return new ItineraryDay
                        {
                            ItineraryDayId = dayId,
                            DayNumber = day.DayNumber,
                            DayTitle = day.DayTitle,
                            WishlistId = query.WishlistId,
                            ItineraryItems = query
                                .ItineraryItems.Where(item => item.DayNumber == day.DayNumber)
                                .Select(item => new ItineraryItem
                                {
                                    ItemDescription = item.ItemDescription,
                                    ItemOrderIndex = item.ItemOrderIndex,
                                    ItemType = item.ItemType,
                                    TimeOfDay = item.TimeOfDay,
                                    ItineraryDayId = dayId,
                                })
                                .ToList(),
                        };
                    })
                    .ToList(),
            };
        }
    }
}
