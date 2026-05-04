using System.Runtime.InteropServices;
using System.Text.Json;
using Hodrac_MVP_Backend.DTOs.Category;
using Hodrac_MVP_Backend.DTOs.Country;
using Hodrac_MVP_Backend.DTOs.Currency;
using Hodrac_MVP_Backend.DTOs.Destination;
using Hodrac_MVP_Backend.DTOs.ItineraryDay;
using Hodrac_MVP_Backend.DTOs.ItineraryItem;
using Hodrac_MVP_Backend.DTOs.Tag;
using Hodrac_MVP_Backend.DTOs.Wishlist;
using Hodrac_MVP_Backend.Enums;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Category;
using Hodrac_MVP_Backend.Mappers.Country;
using Hodrac_MVP_Backend.Mappers.Currency;
using Hodrac_MVP_Backend.Mappers.Destination;
using Hodrac_MVP_Backend.Mappers.Tag;
using Hodrac_MVP_Backend.Models;
using Hodrac_MVP_Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hodrac_MVP_Backend.Data
{
    public static class CustomDatabaseSeeder
    {
        public static async Task SeedCategoryAsync(CustomDbContext context)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "categorySeed.json"
            );
            Console.WriteLine($"Filepath {filePath}");
            if (!File.Exists(filePath))
                return;

            var categoryJsonData = await File.ReadAllTextAsync(filePath);
            var categories = JsonSerializer.Deserialize<CategorySeedDto>(categoryJsonData);

            if (categories == null)
                return;

            var existingKeys = await context.Categories.Select(c => c.Key).ToListAsync();

            var newCategories = categories
                .Categories.Where(c => !existingKeys.Contains(c.Key))
                .Select(c => c.FromJsonDtoToCategory())
                .ToList();

            if (newCategories.IsNullOrEmpty())
                return;

            await context.Categories.AddRangeAsync(newCategories);
            await context.SaveChangesAsync();
        }

        public static async Task SeedTagAsync(CustomDbContext context)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "tagsSeed.json"
            );
            if (!File.Exists(filePath))
                return;

            var tagJsonData = await File.ReadAllTextAsync(filePath);
            var tags = JsonSerializer.Deserialize<TagSeedDto>(tagJsonData);

            if (tags == null)
                return;

            var existingKeys = await context.Tags.Select(c => c.Key).ToListAsync();

            var newTags = tags
                .Tags.Where(t => !existingKeys.Contains(t.Key))
                .Select(t => t.FromJsonDtoToTag())
                .ToList();

            if (newTags.IsNullOrEmpty())
                return;

            await context.Tags.AddRangeAsync(newTags);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCurrencyAsync(CustomDbContext context)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "currencySeed.json"
            );
            if (!File.Exists(filePath))
                return;
            var currencyJsonData = await File.ReadAllTextAsync(filePath);
            var currencyData = JsonSerializer.Deserialize<List<CurrencyJsonDto>>(currencyJsonData);

            if (currencyData == null)
                return;

            var existingCurrencies = await context
                .Currencies.Select(c => c.CurrencyName)
                .ToListAsync();

            var newCurrency = currencyData
                .Where(c => !existingCurrencies.Contains(c.Name))
                .Select(c => c.FromCurrencyJsonToCurrency())
                .ToList();

            if (newCurrency.IsNullOrEmpty())
                return;

            await context.AddRangeAsync(newCurrency);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCountryAsync(CustomDbContext context)
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "countrySeed.json"
            );

            if (!File.Exists(filePath))
                return;

            var countryJsonData = await File.ReadAllTextAsync(filePath);
            var countryData = JsonSerializer.Deserialize<List<CountryJsonDto>>(countryJsonData);

            if (countryData.IsNullOrEmpty())
                return;

            var existingCountries = await context
                .Countries.Select(c => c.CountryName)
                .ToListAsync();

            var newCountries = countryData
                .Where(c => !existingCountries.Contains(c.Name))
                .Select(c => c.FromCountryDtoToCountry());

            if (newCountries.IsNullOrEmpty())
                return;
            await context.AddRangeAsync(newCountries);
            await context.SaveChangesAsync();
        }

        public static async Task SeedWishlist(CustomDbContext context)
        {
            IWishlistRepository wishlistRepo = new WishlistRepository(context);
            var tokyoIds = await context
                .Destinations.Where(d => d.DestinationCities.Any(dc => dc.City.CityName == "Tokyo"))
                .Select(dest => dest.DestinationId)
                .ToListAsync();
            //var sanBlasIds = await context
            //    .Destinations.Where(d =>
            //        d.DestinationCities.Any(dc => dc.City.CityName == "Guna Yala")
            //    )
            //    .Select(dest => dest.DestinationId)
            //    .ToListAsync();
            //Console.WriteLine($"San blas ${sanBlasIds}");
            //var panamaIds = await context
            //    .Destinations.Where(d =>
            //        d.DestinationCities.Any(dc => dc.City.CityName == "Panama City")
            //    )
            //    .Select(dest => dest.DestinationId)
            //    .ToListAsync();
            //var bocasIds = await context
            //    .Destinations.Where(d =>
            //        d.DestinationCities.Any(dc =>
            //            dc.City.CityName == "Caribbean coast of Panama (Northwest)"
            //        )
            //    )
            //    .Select(dest => dest.DestinationId)
            //    .ToListAsync();
            var guidList = new Dictionary<String, List<Guid>>
            {
                { "Tokyo", tokyoIds },
                //{ "San Blas", sanBlasIds },
                //{ "Panama City", panamaIds },
                //{ "Bocas", bocasIds },
            };
            var tokyoWishlist = new QueryWishlistDto
            {
                WishlistId = Guid.NewGuid(),
                WishlistHeroImage =
                    "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/tokyo.jpg",
                WishlistName =
                    "5-Day Tokyo First-Time Trip (Perfect split: shibuya, shinjuku, asakusa)",
                WishlistDescription =
                    "Tokyo in 5 days: Experience the perfect mix of chaos and calm with neon lights, ancient temples, street food, and skyline views.",
                ShortStory =
                    "Here’s a 5 day Tokyo itinerary perfect for first timers wanting to see it all, from relaxing shrines and culturally rich streets to high octane nightlife and immersive art. Each day is thoughtfully planned to include time for exploration and recovery so you never feel overwhelmed, but still get to see the best of Tokyo.",
                TotalDays = 5,
                PeopleType = "Best for solo travelers, couples, and first-time visitors",

                ItineraryDays =
                [
                    new ClientItineraryDayDto
                    {
                        DayNumber = 1,
                        DayTitle = "Harajuku & Shibuya — Tokyo Energy Introduction",
                    },
                    new ClientItineraryDayDto
                    {
                        DayNumber = 2,
                        DayTitle = "Asakusa — Culture & Slow Exploration",
                    },
                    new ClientItineraryDayDto
                    {
                        DayNumber = 3,
                        DayTitle = "Shinjuku — City Balance & Nightlife",
                    },
                    new ClientItineraryDayDto
                    {
                        DayNumber = 4,
                        DayTitle = "Central Tokyo — Food & Immersive Experience",
                    },
                    new ClientItineraryDayDto
                    {
                        DayNumber = 5,
                        DayTitle = "Flex Day — Personalize Your Tokyo Experience",
                    },
                ],

                ItineraryItems =
                [
                    // DAY 1
                    new ClientItineraryItemDto
                    {
                        DayNumber = 1,
                        ItemDescription = "Visit Meiji Shrine (quiet forest shrine)",
                        ItemOrderIndex = 0,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 1,
                        ItemDescription = "Explore Takeshita Street (street food & youth culture)",
                        ItemOrderIndex = 1,
                        ItemType = "Location",
                        TimeOfDay = "Afternoon",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 1,
                        ItemDescription = "Walk through Cat Street (local boutiques & cafes)",
                        ItemOrderIndex = 2,
                        ItemType = "Location",
                        TimeOfDay = "Afternoon",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 1,
                        ItemDescription = "Experience Shibuya Crossing",
                        ItemOrderIndex = 3,
                        ItemType = "Location",
                        TimeOfDay = "Evening",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 1,
                        ItemDescription = "Shibuya Sky (sunset to night city view)",
                        ItemOrderIndex = 4,
                        ItemType = "Location",
                        TimeOfDay = "Evening",
                    },
                    // DAY 2
                    new ClientItineraryItemDto
                    {
                        DayNumber = 2,
                        ItemDescription = "Visit Senso-ji Temple",
                        ItemOrderIndex = 0,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 2,
                        ItemDescription = "Walk through Kaminarimon Gate",
                        ItemOrderIndex = 1,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 2,
                        ItemDescription = "Explore Asakusa streets (souvenirs & food stalls)",
                        ItemOrderIndex = 2,
                        ItemType = "Location",
                        TimeOfDay = "Afternoon",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 2,
                        ItemDescription = "Relax along Sumida River walk",
                        ItemOrderIndex = 3,
                        ItemType = "Location",
                        TimeOfDay = "Evening",
                    },
                    // DAY 3
                    new ClientItineraryItemDto
                    {
                        DayNumber = 3,
                        ItemDescription = "Walk through Shinjuku Gyoen National Garden",
                        ItemOrderIndex = 0,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 3,
                        ItemDescription =
                            "Tokyo Metropolitan Government Building (free skyline view)",
                        ItemOrderIndex = 1,
                        ItemType = "Location",
                        TimeOfDay = "Afternoon",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 3,
                        ItemDescription = "Explore Golden Gai (small bars & local nightlife)",
                        ItemOrderIndex = 2,
                        ItemType = "Location",
                        TimeOfDay = "Evening",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 3,
                        ItemDescription = "Visit Kabukicho (neon nightlife district)",
                        ItemOrderIndex = 3,
                        ItemType = "Location",
                        TimeOfDay = "Evening",
                    },
                    // DAY 4
                    new ClientItineraryItemDto
                    {
                        DayNumber = 4,
                        ItemDescription = "Food crawl at Tsukiji Outer Market",
                        ItemOrderIndex = 0,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 4,
                        ItemDescription = "Visit teamLab Borderless (immersive digital art)",
                        ItemOrderIndex = 1,
                        ItemType = "Location",
                        TimeOfDay = "Afternoon",
                    },
                    // DAY 5 (FLEX)
                    new ClientItineraryItemDto
                    {
                        DayNumber = 5,
                        ItemDescription = "Option A: Tokyo Disney",
                        ItemOrderIndex = 0,
                        ItemType = "Activity",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 5,
                        ItemDescription =
                            "Option B: Explore Akihabara (anime, arcades, electronics)",
                        ItemOrderIndex = 1,
                        ItemType = "Location",
                        TimeOfDay = "Morning",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 5,
                        ItemDescription = "Last-minute shopping",
                        ItemOrderIndex = 2,
                        ItemType = "Activity",
                        TimeOfDay = "Afternoon",
                    },
                    new ClientItineraryItemDto
                    {
                        DayNumber = 5,
                        ItemDescription = "Farewell dinner in Tokyo",
                        ItemOrderIndex = 3,
                        ItemType = "Activity",
                        TimeOfDay = "Evening",
                    },
                ],
            };
            //List<QueryWishlistDto> wishlists =
            //[
            //    new QueryWishlistDto
            //    {
            //        WishlistId = Guid.NewGuid(),
            //        WishlistHeroImage =
            //            "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/san-blas-hero.jpg",
            //        WishlistDescription =
            //            "Turquoise water, hammocks over the sea, and fresh lobster on the sand — no wifi, no worries.",
            //        WishlistName = "San Blas Relax Trip",
            //        ShortStory =
            //            "Spend three days island-hopping through the San Blas archipelago, sleeping in rustic cabins over crystal-clear water, snorkeling shipwrecks, and eating lobster on the sand.",
            //        TotalDays = 3,
            //        PeopleType = "Best for couples / slow travelers",
            //        ItineraryDays =
            //        [
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 1,
            //                DayTitle = "Arrival & First Island Escape",
            //            },
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 2,
            //                DayTitle = "Sandbars & Lobster Lunch",
            //            },
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 3,
            //                DayTitle = "Slow Morning & Return",
            //            },
            //        ],
            //        ItineraryItems =
            //        [
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Drive from Panama City to Cartí",
            //                ItemOrderIndex = 0,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Boat transfer into San Blas",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Arrive at Isla Perro",
            //                ItemOrderIndex = 2,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Snorkel shipwreck reef",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Lunch on the island",
            //                ItemOrderIndex = 4,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Overnight on Isla Diablo",
            //                ItemOrderIndex = 5,
            //                ItemType = "Location",
            //                TimeOfDay = "Evening",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Morning at The Pool sandbar",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Boat to Banedup Island",
            //                ItemOrderIndex = 1,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Lunch at Ibin's Beach Restaurant",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Afternoon snorkeling near Dutch Cays",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Overnight overwater cabin",
            //                ItemOrderIndex = 4,
            //                ItemType = "Location",
            //                TimeOfDay = "Evening",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Breakfast by the sea",
            //                ItemOrderIndex = 0,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Visit Kuna village",
            //                ItemOrderIndex = 1,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Return boat to Cartí",
            //                ItemOrderIndex = 2,
            //                ItemType = "Location",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Drive back to Panama City",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //        ],
            //    },
            //    new QueryWishlistDto
            //    {
            //        WishlistId = Guid.NewGuid(),
            //        WishlistHeroImage =
            //            "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/panamacity.jpeg",
            //        WishlistDescription =
            //            "History, skyline views, the Panama Canal, and fresh seafood — the perfect introduction.",
            //        WishlistName = "Panama City Highlights (Pacific Side)",
            //        ShortStory =
            //            "Spend two days exploring Panama City’s historic streets, walking along the waterfront skyline, and witnessing one of the greatest engineering feats in the world.",
            //        TotalDays = 2,
            //        PeopleType = "Best for first-time visitors / city lovers",

            //        ItineraryDays =
            //        [
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 1,
            //                DayTitle = "Old Town & City Energy",
            //            },
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 2,
            //                DayTitle = "Canal & History of Panama",
            //            },
            //        ],

            //        ItineraryItems =
            //        [
            //            // DAY 1
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Arrive at Casco Viejo",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Walk cobblestone streets and explore plazas",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Visit Palacio de las Garzas",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Lunch at Mercado de Mariscos",
            //                ItemOrderIndex = 3,
            //                ItemType = "Location",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Walk along Cinta Costera",
            //                ItemOrderIndex = 4,
            //                ItemType = "Location",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Sunset overlooking Panama Bay",
            //                ItemOrderIndex = 5,
            //                ItemType = "Activity",
            //                TimeOfDay = "Evening",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Rooftop bar in Casco Viejo",
            //                ItemOrderIndex = 6,
            //                ItemType = "Activity",
            //                TimeOfDay = "Night",
            //            },
            //            // DAY 2
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Visit Panama Canal (Miraflores Locks)",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Watch ships pass through the locks",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Explore the Panama Canal Museum",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Lunch near the canal",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Visit Biomuseo",
            //                ItemOrderIndex = 4,
            //                ItemType = "Location",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Panoramic city and canal views",
            //                ItemOrderIndex = 5,
            //                ItemType = "Activity",
            //                TimeOfDay = "Evening",
            //            },
            //        ],
            //    },
            //    new QueryWishlistDto
            //    {
            //        WishlistId = Guid.NewGuid(),
            //        WishlistHeroImage =
            //            "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/bocas.jpeg",
            //        WishlistDescription =
            //            "Caribbean rhythm, boat parties, and world-class waves — Panama’s wild side.",
            //        WishlistName = "Bocas del Toro Party & Surf",
            //        ShortStory =
            //            "Spend four days hopping between islands, surfing tropical waves, and partying on boats with travelers from around the world.",
            //        TotalDays = 4,
            //        PeopleType = "Best for backpackers / party travelers / surfers",

            //        ItineraryDays =
            //        [
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 1,
            //                DayTitle = "Arrival & Island Vibes",
            //            },
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 2,
            //                DayTitle = "Filthy Friday Party",
            //            },
            //            new ClientItineraryDayDto { DayNumber = 3, DayTitle = "Beaches & Surf" },
            //            new ClientItineraryDayDto
            //            {
            //                DayNumber = 4,
            //                DayTitle = "Snorkel & Departure",
            //            },
            //        ],

            //        ItineraryItems =
            //        [
            //            // DAY 1
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Arrive in Bocas Town (Isla Colón)",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Check into hostel or hotel",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Explore town and waterfront",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Evening",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 1,
            //                ItemDescription = "Dinner and drinks in Bocas Town",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Night",
            //            },
            //            // DAY 2
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Join Filthy Friday boat party",
            //                ItemOrderIndex = 0,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Island hopping with drinks and music",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Swim stops in crystal clear water",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 2,
            //                ItemDescription = "Return and recover in town",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Evening",
            //            },
            //            // DAY 3
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Morning surf at Red Frog Beach",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Explore jungle and find red frogs",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Visit Starfish Beach",
            //                ItemOrderIndex = 2,
            //                ItemType = "Location",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Relax in shallow water and hammocks",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 3,
            //                ItemDescription = "Sunset at Wizard Beach",
            //                ItemOrderIndex = 4,
            //                ItemType = "Location",
            //                TimeOfDay = "Evening",
            //            },
            //            // DAY 4
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 4,
            //                ItemDescription = "Boat to Cayos Zapatilla",
            //                ItemOrderIndex = 0,
            //                ItemType = "Location",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 4,
            //                ItemDescription = "Snorkel coral reefs and swim",
            //                ItemOrderIndex = 1,
            //                ItemType = "Activity",
            //                TimeOfDay = "Morning",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 4,
            //                ItemDescription = "Relax on the beach",
            //                ItemOrderIndex = 2,
            //                ItemType = "Activity",
            //                TimeOfDay = "Noon",
            //            },
            //            new ClientItineraryItemDto
            //            {
            //                DayNumber = 4,
            //                ItemDescription = "Return to Bocas Town and depart",
            //                ItemOrderIndex = 3,
            //                ItemType = "Activity",
            //                TimeOfDay = "Afternoon",
            //            },
            //        ],
            //    },
            //];

            //if (await context.Wishlists.AnyAsync())
            //    return;

            await wishlistRepo.CreateNewWishlist(tokyoWishlist, guidList["Tokyo"]);

            //await wishlistRepo.CreateNewWishlist(wishlists[0], guidList["San Blas"]);
            //await wishlistRepo.CreateNewWishlist(wishlists[1], guidList["Panama City"]);
            //await wishlistRepo.CreateNewWishlist(wishlists[2], guidList["Bocas"]);
        }

        public static async Task SeedDestination(CustomDbContext context)
        {
            var japanCountryId = await context
                .Countries.Where(c => c.CountryName == "Japan")
                .Select(c => c.CountryId)
                .FirstOrDefaultAsync();
            var panamaCountryId = await context
                .Countries.Where(c => c.CountryName == "Panama")
                .Select(c => c.CountryId)
                .FirstOrDefaultAsync();
            var tokyoCityIds = await context
                .Cities.Where(c => c.CityName == "Tokyo")
                .Select(c => c.CityId)
                .ToListAsync();
            var gunaCityIds = await context
                .Cities.Where(c => c.CityName == "Guna Yala")
                .Select(c => c.CityId)
                .ToListAsync();
            var cariCityIds = await context
                .Cities.Where(c => c.CityName == "Caribbean coast of Panama (Northwest)")
                .Select(c => c.CityId)
                .ToListAsync();
            var panamaCityIds = await context
                .Cities.Where(c => c.CityName == "Panama City")
                .Select(c => c.CityId)
                .ToListAsync();
            var japanLangIds = await context
                .Languages.Where(l => l.LanguageName == "Japanese")
                .Select(l => l.LanguageId)
                .ToListAsync();
            var panamaLangIds = await context
                .Languages.Where(l => l.LanguageName == "Spanish" || l.LanguageName == "English")
                .Select(l => l.LanguageId)
                .ToListAsync();
            var gunaLangIds = await context
                .Languages.Where(l =>
                    l.LanguageName == "Spanish" || l.LanguageName == "Dulegaya (Guna/Kuna)"
                )
                .Select(l => l.LanguageId)
                .ToListAsync();

            var cartiLangIds = await context
                .Languages.Where(l =>
                    l.LanguageName == "Spanish"
                    || l.LanguageName == "Dulegaya (Guna/Kuna)"
                    || l.LanguageName == "English"
                )
                .Select(l => l.LanguageId)
                .ToListAsync();

            var japanCurrencyId = await context
                .Currencies.Where(c => c.CurrencySymbol == "JPY")
                .Select(c => c.CurrencyId)
                .ToListAsync();
            var panamaCurrencyId = await context
                .Currencies.Where(c => c.CurrencySymbol == "USD" || c.CurrencySymbol == "PAB")
                .Select(c => c.CurrencyId)
                .ToListAsync();
            var kuromonCatIds = await context
                .Categories.Where(c => c.Key == "market_street_life")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var cascoCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var canalCatIds = await context
                .Categories.Where(c => c.Key == "landmark_monument")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var cintaCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var mercadoCatIds = await context
                .Categories.Where(c => c.Key == "market_street_life")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var museoCatIds = await context
                .Categories.Where(c => c.Key == "landmark_monument")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var bocasCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var redCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var starfishCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var wizardCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var zapaCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var ibinCatIds = await context
                .Categories.Where(c => c.Key == "food_experience")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var perroCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var diabloCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var cartiCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var holanCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var kuromonTagIds = await context
                .Tags.Where(t => t.Key == "food_focused" || t.Key == "local_favorite")
                .Select(t => t.TagId)
                .ToListAsync();
            var cascoTagIds = await context
                .Tags.Where(t => t.Key == "walkable" || t.Key == "history" || t.Key == "cultural")
                .Select(t => t.TagId)
                .ToListAsync();
            var canalTagIds = await context
                .Tags.Where(t =>
                    t.Key == "tourist_hotspot"
                    || t.Key == "educational"
                    || t.Key == "family_friendly"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var cintaTagIds = await context
                .Tags.Where(t =>
                    t.Key == "seasonal" || t.Key == "family_friendly" || t.Key == "walkable"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var mercadoTagIds = await context
                .Tags.Where(t => t.Key == "food_focused")
                .Select(t => t.TagId)
                .ToListAsync();
            var museoTagIds = await context
                .Tags.Where(t =>
                    t.Key == "architecture" || t.Key == "nature" || t.Key == "educational"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var bocasTagIds = await context
                .Tags.Where(t =>
                    t.Key == "nightlife"
                    || t.Key == "best_evening"
                    || t.Key == "budget_friendly"
                    || t.Key == "walkable"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var redTagIds = await context
                .Tags.Where(t =>
                    t.Key == "good_short_visit"
                    || t.Key == "premium"
                    || t.Key == "budget_friendly"
                    || t.Key == "nature"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var starfishTagIds = await context
                .Tags.Where(t =>
                    t.Key == "nature" || t.Key == "group_friendly" || t.Key == "solo_friendly"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var wizardTagIds = await context
                .Tags.Where(t =>
                    t.Key == "adventurous" || t.Key == "best_morning" || t.Key == "family_friendly"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var zapaTagIds = await context
                .Tags.Where(t => t.Key == "tourist_hotspot" || t.Key == "nature")
                .Select(t => t.TagId)
                .ToListAsync();
            var ibinTagIds = await context
                .Tags.Where(t =>
                    t.Key == "local_favorite" || t.Key == "hidden_gem" || t.Key == "food_focused"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var perroTagIds = await context
                .Tags.Where(t =>
                    t.Key == "photography"
                    || t.Key == "adventurous"
                    || t.Key == "group_friendly"
                    || t.Key == "nature"
                    || t.Key == "good_short_visit"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var diabloTagIds = await context
                .Tags.Where(t =>
                    t.Key == "couple_friendly"
                    || t.Key == "relaxing"
                    || t.Key == "nature"
                    || t.Key == "group_friendly"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var cartiTagIds = await context
                .Tags.Where(t => t.Key == "crowded" || t.Key == "tourist_hotspot")
                .Select(t => t.TagId)
                .ToListAsync();
            var holanTagIds = await context
                .Tags.Where(t =>
                    t.Key == "best_morning"
                    || t.Key == "premium"
                    || t.Key == "good_short_visit"
                    || t.Key == "nature"
                )
                .Select(t => t.TagId)
                .ToListAsync();

            //var destinationDto = new DestinationDto
            //{
            //    DestinationName = "Kuromon Market",
            //    DestinationImage = "/images/destinations/kuromon_market.jpg",
            //    Description = new DescriptionJsonDto { },
            //    BestPeriodToVisit = new List<TravelPeriod>
            //    {
            //        TravelPeriod.AprToJun,
            //        TravelPeriod.OctToDec,
            //    },
            //    MaxCost = 30,
            //    MinCost = 15,
            //    SafetyLevel = 10,
            //    TimeZone = "Japan Standard Time",
            //    CountryId = japanCountryId,
            //    CityIds = osakaCityIds,
            //    LanguageIds = japanLangIds,
            //    CurrencyIds = japanCurrencyId,
            //    CategoryIds = kuromonCatIds,
            //    TagIds = kuromonTagIds,
            //};
            var meijiCatIds = await context
                .Categories.Where(c => c.Key == "cultural_site")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var meijiTagIds = await context
                .Tags.Where(t =>
                    t.Key == "cultural"
                    || t.Key == "walkable"
                    || t.Key == "history"
                    || t.Key == "budget_friendly"
                    || t.Key == "photography"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var takeshitaCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var takeshitaTagIds = await context
                .Tags.Where(t =>
                    t.Key == "cultural"
                    || t.Key == "walkable"
                    || t.Key == "shopping"
                    || t.Key == "budget_friendly"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "crowded"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var shibuyaSkyCatIds = await context
                .Categories.Where(c => c.Key == "viewpoint_scenic_spot")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var shibuyaSkyTagIds = await context
                .Tags.Where(t =>
                    t.Key == "architecture" || t.Key == "photography" || t.Key == "tourist_hotspot"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var crossingCatIds = await context
                .Categories.Where(c => c.Key == "landmark_monument")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var crossingTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "crowded"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "photography"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var catStreetCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var catStreetTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "shopping"
                    || t.Key == "premium"
                    || t.Key == "tourist_hotspot"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var riverwalkCatIds = await context
                .Categories.Where(c => c.Key == "activity_experience")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var riverwalkTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "romantic"
                    || t.Key == "photography"
                    || t.Key == "couple_friendly"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var asakusaStreetsCatIds = await context
                .Categories.Where(c => c.Key == "market_street_life")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var asakusaStreetsTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "crowded"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "shopping"
                    || t.Key == "food_focused"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var kaminarimonCatIds = await context
                .Categories.Where(c => c.Key == "landmark_monument")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var kaminarimonTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "crowded"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "photography"
                    || t.Key == "cultural"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var sensojiCatIds = await context
                .Categories.Where(c => c.Key == "cultural_site")
                .Select(c => c.CategoryId)
                .ToListAsync();
            var tsukijiCatIds = await context
                .Categories.Where(c => c.Key == "market_street_life")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var tsukijiTagIds = await context
                .Tags.Where(t =>
                    t.Key == "crowded"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "food_focused"
                    || t.Key == "walkable"
                )
                .Select(t => t.TagId)
                .ToListAsync();

            var sensojiTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "crowded"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "cultural"
                    || t.Key == "shopping"
                    || t.Key == "photography"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var kabukichoCatIds = await context
                .Categories.Where(c => c.Key == "entertainment_nightlife")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var kabukichoTagIds = await context
                .Tags.Where(t =>
                    t.Key == "nightlife" || t.Key == "best_at_night" || t.Key == "social"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var goldenGaiCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var goldenGaiTagIds = await context
                .Tags.Where(t =>
                    t.Key == "nightlife"
                    || t.Key == "best_at_night"
                    || t.Key == "group_friendly"
                    || t.Key == "social"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var gyoenCatIds = await context
                .Categories.Where(c => c.Key == "nature_outdoor")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var gyoenTagIds = await context
                .Tags.Where(t =>
                    t.Key == "walkable"
                    || t.Key == "photography"
                    || t.Key == "nature"
                    || t.Key == "history"
                )
                .Select(t => t.TagId)
                .ToListAsync();

            var metroGovCatIds = await context
                .Categories.Where(c => c.Key == "viewpoint_scenic_spot")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var metroGovTagIds = await context
                .Tags.Where(t =>
                    t.Key == "photography" || t.Key == "budget_friendly" || t.Key == "architecture"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var akihabaraCatIds = await context
                .Categories.Where(c => c.Key == "neighborhood_district")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var akihabaraTagIds = await context
                .Tags.Where(t =>
                    t.Key == "cultural" || t.Key == "shopping" || t.Key == "tourist_hotspot"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var disneyCatIds = await context
                .Categories.Where(c => c.Key == "activity_experience")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var disneyTagIds = await context
                .Tags.Where(t =>
                    t.Key == "family_friendly"
                    || t.Key == "premium"
                    || t.Key == "tourist_hotspot"
                    || t.Key == "crowded"
                )
                .Select(t => t.TagId)
                .ToListAsync();
            var teamlabCatIds = await context
                .Categories.Where(c => c.Key == "viewpoint_scenic_spot")
                .Select(c => c.CategoryId)
                .ToListAsync();

            var teamlabTagIds = await context
                .Tags.Where(t =>
                    t.Key == "architecture" || t.Key == "photography" || t.Key == "tourist_hotspot"
                )
                .Select(t => t.TagId)
                .ToListAsync();

            List<DestinationDto> destinationList =
            [
                new DestinationDto
                {
                    DestinationName = "Casco Viejo",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/casco.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "I’ve often noticed that some neighborhoods don’t just hold history; they wear"
                            + " it openly on every corner. Casco Viejo, or San Felipe, is exactly that a 40-acre peninsula"
                            + " where the 17th-century fortifications built to keep pirates out now invite the world in. It"
                            + " is a place of contrast, where neoclassical and Afro-Antillean buildings are layered directly onto"
                            + " colonial ruins, creating a skyline that feels like a conversation between centuries.\r\nWhether you"
                            + " are navigating the cobblestone streets during the heat of the day or watching the Pacific sunset from"
                            + " a rooftop, the neighborhood demands that you slow down and experience it on foot.\r\nWhat to Do"
                            + ":\r\nWalking Tour: Follow a self-guided route through the 40-acre historic district to see the"
                            + " Metropolitan Cathedral, which took over 100 years to build.\r\nMuseums: Visit the Panama Canal"
                            + " Museum or the Museo de la Mola to understand the engineering and cultural foundations of the country."
                            + "\r\nGovernmental Sights: Walk past the Palacio de las Garzas, the official residence and office of the"
                            + " President.\r\nSunset Rituals: Head to one of the many rooftop bars to watch the sunset paint the modern"
                            + " city skyline gold while you sit in the historic heart.\r\nCoffee Culture: Sample Panama’s world-famous"
                            + " Geisha coffee at locally-owned specialty shops.\r\n",
                        Directions =
                            "Location: Also known as San Felipe, it is the city’s original walled quarter,"
                            + " located on a peninsula isolated by the sea and a defensive system of walls.\r\nProximity:"
                            + " It is a compact neighborhood situated just minutes away from most central Panama City hotels."
                            + "\r\nGetting Around: The area is best experienced on foot; a self-guided walking route is recommended"
                            + " to navigate the layered cobblestone streets and hidden plazas.\r\n",
                        WhatToKnow =
                            "Duration: Plan for more than 3 hours to see the main sights;"
                            + " a full day is better to include lunch and museums.\r\nWalking is Essential: Nearly everything"
                            + " is within walking distance, and the narrow streets are better suited for pedestrians than cars."
                            + "\r\nFamily Friendly: Plaza Bolívar is a local favorite for families due to its shaded benches and"
                            + " nearby gelato shops.\r\n",
                        ThingsToBeWaryOf =
                            "Night Safety: While safe during the day, it is recommended to stick to main streets"
                            + " near plazas and hotels after dark for the best experience.\r\nHigher Pricing: As a tourist-heavy area,"
                            + " expect prices for food and services to be higher than in the rest of Panama City.\r\nGentrification: The"
                            + " neighborhood has experienced significant gentrification in recent years, which creates a sharp contrast"
                            + " between restored luxury areas and historic ruins.\r\nPirate History (Contextual): The original city was"
                            + " destroyed by pirates, and the current quarter was built as a defensive stronghold; some areas may still"
                            + " feel very enclosed or narrow.\r\n",
                        LocalPerspective =
                            "The \"360\" View: Locals who have lived here since 2008 describe it as a"
                            + " \"compact, layered neighborhood\" that is best felt rather than just seen.\r\nThe Quiet Caretakers:"
                            + " While tourists look at the cathedrals, locals look for the \"Casco Cat Community\"—a group that cares"
                            + " for the neighborhood's street cats, who are considered the quiet guardians of the district.\r\nAuthentic"
                            + " Art: Beyond the souvenir shops, look for genuine Kuna Molas. These are hand-stitched textiles created by"
                            + " indigenous Guna women that serve as intricate maps of their heritage and symbolism.\r\nHistorical Resilience:"
                            + " Locals take pride in the fact that this city was founded in 1673 as a direct response to the destruction of"
                            + " \"Panamá Viejo\" by the pirate Henry Morgan.\r\n",
                        HiddenCost =
                            "Daily Expenses: A daily trip typically ranges from $63 (budget) to $189+ (mid-range) per person,"
                            + " covering accommodation and food.\r\nPremium Dining: Meals often cost between $7 and $14, but luxury experiences"
                            + " can exceed $630.\r\nSpecialty Items: High-end products like Panama’s world-famous Geisha coffee or hand-stitched"
                            + " Kuna Molas are premium purchases.\r\nTour Access: Digital or instant-access walking maps and guided services may"
                            + " involve a separate purchase fee.\r\n",
                        NearbyComplements =
                        [
                            "Mercado de Mariscos: A short walk or drive away, perfect for eating fresh ceviche and fried fish with the"
                                + " locals.",
                            "Cinta Costera: The waterfront promenade that offers a modern contrast to Casco’s colonial charm.",
                            "Ancon Hill: A 30–45 minute hike nearby that offers panoramic views of the city, the canal, and the historic"
                                + " district from above.",
                        ],
                        BestTimeToVisit =
                            "Late Afternoon: Arrive around 4:00 PM to explore the churches and plazas before the heat"
                            + " breaks and the rooftop nightlife begins.\r\nYear-round: The neighborhood hosts over 50 yearly events, ensuring"
                            + " there is almost always a festival or parade occurring.\r\n",
                        crowdLevel =
                            "High (8/10): It is recognized as one of the most visited neighborhoods in Panama.\r\nPeak Times:"
                            + " Nightlife venues, rooftop bars, and plazas attract both international travelers and locals, especially on weekends."
                            + "\r\nAtmosphere: The streets are consistently lively with shops, cultural sites, and 60+ restaurants.\r\n",
                        Accessibility =
                            "Rating: 8/10\r\nCasco Viejo is significantly more accessible than the remote islands or mountain"
                            + " ports of Panama, though its historic nature presents specific trade-offs.\r\nWalkability: Because the district"
                            + " covers only about 40 acres, it is arguably the most walkable neighborhood in the country. You can move between"
                            + " major landmarks, restaurants, and plazas in just a few minutes of easy walking.\r\nPedestrian Infrastructure:"
                            + " The neighborhood is designed for foot traffic. Many streets are either pedestrian-only or have restricted vehicle"
                            + " access, allowing you to explore without the constant pressure of heavy city traffic.\r\nThe Historical Trade-off:"
                            + " The reason it is not a 10/10 is due to the preserved infrastructure. The streets are made of original, often uneven"
                            + " cobblestones, and the sidewalks can be narrow or vary in height. This can make the terrain a bit challenging for"
                            + " strollers, wheelchairs, or those with significant mobility issues.\r\nTransit Access: While driving into the district"
                            + " is difficult due to narrow lanes and a lack of parking, it is very easy to reach via a quick ride-share or taxi from"
                            + " the modern city center. Once you arrive at one of the main entry points, the entire area is at your doorstep.\r\nIn"
                            + " short, if you are exploring on foot, it is an exceptionally accessible \"outdoor museum,\" but the historic paving"
                            + " requires a bit of attention to where you step.\r\n",
                        IdealDuration =
                            "Standard Visit: More than 3 hours is required to see the primary churches, plazas, and museums."
                            + "\r\nWalking Tour: A full self-guided walking tour is designed to last up to 4 hours.\r\nFlexible Pace: Many visitors"
                            + " prefer a relaxed half-day or breaking the visit into several stops between meals and photos.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod> { TravelPeriod.YearRound },
                    MaxCost = 190,
                    MinCost = 65,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = panamaCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = cascoCatIds,
                    TagIds = cascoTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Panama Canal Miraflores Locks",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/panamacanal.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "I’ve often thought that some of the greatest human achievements are the ones that fundamentally change the shape of the world.\r\nThe Panama Canal is not just a shortcut between two oceans; it is a monument to the sheer force of will. It is a place where you can stand on a platform and watch a 100,000-ton vessel be lifted by nothing more than gravity and water, moving through a narrow corridor carved out of solid volcanic rock.\r\nIt is a reminder that while nature is powerful, human ingenuity when paired with a century of persistence is capable of moving mountains. Literally.\r\n",
                        Directions =
                            "Location: The Miraflores Visitor Center is located at the Pacific end of the canal, just north of central Panama City.\r\nGetting There: From Panama City, it is roughly a 20–30 minute drive. It is highly recommended to use Waze over Google Maps to navigate local traffic and the specific entrance turns.\r\nKey Stops: If driving yourself, stop at the Amador Causeway or the Bridge of the Americas lookout on the way back for a different perspective of the ships entering the canal.\r\n",
                        WhatToKnow =
                            "The Museum: The visitor center features four floors of exhibits covering everything from the biodiversity workers encountered to the technical mechanics of the original lock gates.\r\nThe IMAX Experience: Your ticket includes a 45-minute 3D film narrated by Morgan Freeman. It provides a cinematic look at the history and the massive \"Expansion Project\" completed in 2016.\r\nNew vs. Old: From the Miraflores platform, you can see the original locks and the newer, widened locks. The original locks use gravity, while the new ones utilize water-saving basins.\r\nThe Mules: Watch for the silver \"electric mules\" (locomotives) that run on rails alongside the ships. They don't pull the ships; they use high-tension cables to keep them perfectly centered in the narrow chambers.\r\n",
                        ThingsToBeWaryOf =
                            "The Heat: The viewing platforms are outdoors and can get very hot and humid. Arrive early or stay hydrated.\r\nShip Schedules: Transits are managed by the Canal Authority and can vary. There is never a 100% guarantee of a ship passage, though the windows mentioned above are your best bet.\r\nCrowds: The center has a capacity of 450 people on the platform, and it fills up quickly when a large Neo-Panamax ship begins its transit.\r\n",
                        LocalPerspective =
                            "The \"Window\" Strategy: Locals know that the locks are not a constant parade. There are two primary \"windows\" for ship transits: the Morning Window (8:00 AM – 9:00 AM) for ships moving toward the Atlantic, and the Afternoon Window (starting at 2:00 PM) when the traffic flips. If you arrive at noon, you might just be looking at a very impressive, but empty, concrete bathtub.\r\nAncon Hill’s Spirit: While tourists look at the ships, locals look at the hill. Ancon Hill, topped with the largest Panamanian flag in existence, was the highest point used by the builders to overlook the progress. It remains a symbol of Panamanian sovereignty over the canal zone.\r\nThe \"3-Cent\" Swim: A favorite piece of local trivia is the story of Richard Halliburton, who in 1928 swam the length of the canal and was charged a toll of exactly 36 cents based on his weight—the lowest toll ever paid.\r\nMiraflores Etymology: The name translates to \"Behold the flowers\" or \"Watch the flowers,\" a nod to the lush, vibrant flora the original laborers encountered while clearing the jungle.\r\n",
                        HiddenCost =
                            "Tourist vs. Local Pricing: There is a significant price gap. International adults pay $17.22, while locals and residents pay $3.00.\r\nParking: While there is a large lot, it can fill up with tour coaches during peak hours.\r\nThe \"Bridge\" Trade-off: Note that on some afternoon tours, the Bridge of the Americas lookout may not be accessible due to traffic patterns or tour scheduling.\r\n",
                        NearbyComplements =
                        [
                            "Metropolitan Natural Park: A rainforest located right inside the city. You can hike to the lookout to see the canal and the city skyline simultaneously.",
                            "Biomuseo: Designed by Frank Gehry, this colorful museum on the Causeway tells the story of how the Isthmus of Panama changed the world's climate and biodiversity.",
                            "Agua Clara Locks (The Wildcard): If you want a less crowded experience, head to the Atlantic side. The Agu.",
                        ],
                        BestTimeToVisit =
                            "8:00 AM: Be there when the doors open to catch the morning transit and beat the midday heat.",
                        crowdLevel =
                            "High (9/10): This is Panama’s most visited landmark. Expect large groups and full viewing decks during ship transits.",
                        Accessibility =
                            "Rating: 9/10: This is one of the most accessible sites in Panama. The visitor center is equipped with ramps, elevators to all four floors, and a dedicated lower platform with full wheelchair access.",
                        IdealDuration =
                            "2–3 Hours: This allows time for the 45-minute IMAX film, a walk through the 4-floor museum, and at least an hour on the viewing deck to hopefully catch a ship transit.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 170,
                    MinCost = 150,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = panamaCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = canalCatIds,
                    TagIds = canalTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Cinta Costera",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/cintacostera.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "To experience the full scale of Panama City’s modern transformation, there is no better vantage point than the Cinta Costera. This 7-kilometer (4.3-mile) coastal beltway is a masterpiece of land reclamation, connecting the glittering glass skyscrapers of Paitilla to the historic colonial walls of Casco Viejo.\r\nWhether you are cycling the marine viaduct that curves over the Pacific or sampling ceviche at the edge of the fish market, the \"Cinta\" serves as the city’s communal backyard a place where the urban energy of Panama meets the open horizon of the bay.\r\n",
                        Directions =
                            "Starting Point: The beltway (Route 1) begins in the heart of the city in the Paitilla neighborhood.\r\nRoute: It extends 7 km (4.3 miles) along the shores of Panama Bay, divided into three sections (Cinta Costera 1, 2, and 3).\r\nEnding Point: It concludes at the Old Town (Casco Viejo) and the El Chorrillo neighborhood, following a 2.5-kilometer marine viaduct that encircles the historic district.\r\nTransport: You can reach any section of the promenade by taxi or public transportation.\r\n",
                        WhatToKnow =
                            "Shade is Scarce: Avoid the \"rookie mistake\" of visiting at noon. The palm-lined paths offer very little protection from the vertical sun, making midday visits intensely hot.\r\nActive Recreation: You can rent bicycles or skates at various points along the first phase of the beltway. There are dedicated lanes for both pedestrians and cyclists to ensure a smooth flow of traffic.\r\n",
                        ThingsToBeWaryOf =
                            "gather, which can make the lanes quite busy with joggers and skaters.\r\nSun Exposure: As a 15-acre landfill breakwater and \"coastal strip\" along the bay, it is heavily exposed to the sun; midday can be very hot.\r\nNeighborhood Transitions: The path leads directly into El Chorrillo, a traditional neighborhood that contrasts with the luxury skyscrapers of Paitilla.\r\n",
                        LocalPerspective =
                            "The Raspa’o Ritual: Don't just get any shaved ice; look for vendors in Anayansi Square at the end of the first kilometer. Ask for the \"cucurucho\" style with extra condensed milk—it’s the definitive local treat for a hot afternoon.\r\nThe Marine Viaduct: This 2.5-kilometer section of Cinta Costera III literally wraps around the historic district. It offers a unique \"floating\" perspective of Casco Viejo’s walls that you cannot get from inside the old city itself.\r\nSabores de El Chorrillo: For a truly authentic meal, skip the upscale hotel restaurants and head to this collection of open-air stalls at the end of the marine section. It is run by traditional cooks from the neighborhood who specialize in whole fried fish and plantains.\r\nThe Skyline Contrast: Stand at the Mirador del Pacífico (Pacific Lookout). From here, you can see the juxtaposition of Panama’s history: the 17th-century ruins to your right and the 21st-century architectural marvels to your left.\r\n",
                        HiddenCost =
                            "Entry Fee: None. It is a \"public recreation area\" and free to the public.\r\nFood and Extras: Costs are limited to what you choose to buy, such as \"cucurucho de raspados,\" street food, or seafood plates at the local markets.\r\nTransportation: Standard taxi or public transit fares to reach the Paitilla or Casco Viejo ends of the beltway.\r\n",
                        NearbyComplements =
                        [
                            "Anayansi Square: Found at the end of the first kilometer, it is the best spot to find \"raspa'o\" (shaved ice with condensed milk).",
                            "Mirador del Pacífico: A major lookout at kilometer 2.6 that offers panoramic views of the modern skyline and the old colonial quarter.",
                            "Mercado del Marisco (Seafood Market): Located near the Pacific Lookout, this is the go-to spot for fresh ceviche.",
                            "Sabores de El Chorrillo: A seaside collection of restaurants run by local cooks at the end of the marine section, famous for fried fish and plantains.",
                            "Maracaná Stadium: A key landmark located at the edge of the Cinta Costera III expansion.",
                        ],
                        BestTimeToVisit =
                            "Daily: 5:00 PM (Golden Hour) is the ideal time. The tropical heat breaks, the \"look-at-me\" skyscrapers begin to glow in the sunset, and the park fills with local life. Early mornings are also excellent for a cooler, quieter experience.\r\nSeasonally: January through March offers the most consistent breeze and blue skies, though the park is vibrant year-round.\r\n",
                        crowdLevel =
                            "High 9/10 (especially on weekends): The promenade is described as a \"very popular spot\" that \"brings together all of Panama\".\r\nAtmosphere: On weekends, it becomes a \"mash-up\" of the entire city’s population, from families slurping ice cream to locals jogging or skating.\r\n",
                        Accessibility =
                            "High 10/10: It is a 26-hectare public recreational space designed specifically for use on foot or by bicycle.\r\nInfrastructure: The beltway includes dedicated bike lanes, wide pedestrian paths, and specialized viewpoints (\"tourist platters\") equipped with sun loungers and binoculars.\r\nAccess Points: Multiple sections are easily accessible from the city center via the pedestrian path on the marine viaduct or the main highway (Avenida Balboa).\r\n",
                        IdealDuration =
                            "1.5 to 3 Hours: While the length is 7 km, the \"suggested trip plan\" recommends visiting for a sunset stroll to watch the city skyscrapers turn gold.\r\nActivity Dependent: A full bike ride or walk including a stop for lunch at the seafood market can easily fill half a day.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod> { TravelPeriod.YearRound },
                    MaxCost = 15,
                    MinCost = 10,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = panamaCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = cintaCatIds,
                    TagIds = cintaTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Mercado de Mariscos",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/mercado.jpeg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "The Mercado de Mariscos (Seafood Market) is the sensory epicenter of Panama City. It’s a place where the salt-crusted reality of the Pacific meets the culinary pulse of the capital. Built in 1995 with the help of the Japanese government, it isn’t a sanitized tourist trap—it’s a working market where you’ll dodge fishermen hauling blue marlin and amberjack while Latin music blares from jury-rigged speakers.\r\nIf Casco Viejo is the city's soul and the Canal is its brain, this market is its stomach.\r\n",
                        Directions =
                            "Location: Situated on Avenida Balboa at the pivot point between the Cinta Costera and the entrance to Casco Viejo.\r\nThe \"Local\" Path: While you can risk crossing the busy highway, the more pleasant and safer route is to follow the waterfront boardwalk. It goes under the highway and around the point, leading you directly from the historic district to the market.\r\nHours: The \"Market Proper\" opens as early as 5:00 AM when the boats arrive. The restaurants serve through lunch and into the evening.\r\n",
                        WhatToKnow =
                            "The Two Sections: The facility is divided into the indoor market proper (raw fish, lobsters, and shellfish on ice) and the outdoor restaurant area (plastic tables, music, and prepared dishes).\r\nCeviche Varieties: It is the local specialty. You can find everything from standard corvina (sea bass) to octopus, shrimp, and even exotic \"royal red shrimp.\"\r\nDining Style: This is not a \"fancy joint.\" Expect a casual environment with \"Latin tunes blaring,\" plastic seating, and \"waiters competing for your business.\"\r\nThe \"Japan Connection\": The modern concrete structure was built in 1995 as a collaboration with the Japan Technical Cooperation Agency.\r\n",
                        ThingsToBeWaryOf =
                            "The Aroma: It is a fish market. The \"scent of the sea\" is powerful and unapologetic. If you have a sensitive stomach, stick to the outdoor seating areas.\r\nBasic Amenities: The bathroom facilities are notoriously basic. You may want to plan your \"pit stops\" before or after your visit.\r\nThe \"Hustle\": It’s a high-energy environment. Expect noise, rhythmic filleting knives, and friendly but persistent haggling from restaurant staff.\r\n",
                        LocalPerspective =
                            "The Two Halves: To the left is the Market Proper—an indoor, chilly hall where the raw catch is sold. To the right are the Outdoor Eateries—rowdy, casual stalls with plastic chairs where the same catch is served fried or cured.\r\nThe Ceviche Ritual: Don't look for a fancy menu. Order a cup of Corvina (sea bass) or Pulpo (octopus) ceviche. It’s served in a disposable cup with a side of saltine crackers. At $1–$2 a cup, it’s arguably the best value-for-money meal in Central America.\r\nVendor Rivalry: As you walk through the outdoor section, vendors will compete for your business, each claiming their ceviche is the \"best.\" Pro-tip: There isn’t much quality difference between stalls; just pick one with a lively vibe and an open table.\r\n",
                        HiddenCost =
                            "Pricing Tiers: * Cups ($1–$2): Ceviche served in disposable cups is the most affordable way to eat.\r\nPlatters ($7–$15+): Fried fish or mixed seafood platters are more expensive than the cups but still considered \"affordable\" compared to hotel dining.\r\nSide Items: Things like fries (papas fritas) or extra patacones (fried plantains) are usually separate additions to your bill.\r\nBathroom Access: In many public markets of this style, there is often a nominal fee (usually $0.25) to use the restroom facilities.\r\nParking: While located near the Cinta Costera, parking in this high-traffic area can be difficult and may involve small tips for informal \"parking attendants\" if you aren't using a taxi.\r\n",
                        NearbyComplements =
                        [
                            "Cinta Costera: Walk off your lunch along the 7km promenade that starts right at the market's doorstep.",
                            "Casco Viejo: Explore the historic plazas just a 10-minute stroll away.",
                            "Biomuseo: A short taxi or bike ride down the Amador Causeway to see the Frank Gehry-designed museum.",
                        ],
                        BestTimeToVisit =
                            "For the Spectacle (5:00 AM – 9:00 AM): Visit in the early morning to see the fishermen unloading their overnight haul and the \"mosaics\" of fresh lobster and snapper being laid out on ice.\r\nFor the Vibe (Weekdays at Lunch): Perfect for a fresh, affordable meal without the intense weekend crowds.\r\nAvoid (Weekend Evenings): Unless you love high-energy crowds and loud music, as this is when the locals come out in force.\r\n",
                        crowdLevel =
                            "Moderate to High (8/10): As a \"bustling hub\" and the city's principal seafood trading operation, it is almost always lively.\r\nPeak Times: The market is most crowded on weekends, when locals \"come out in force\" along the waterfront.\r\nMorning Rush: The indoor market is busiest between 5:00 AM and 9:00 AM as the overnight catch arrives and vendors begin haggling.\r\n",
                        Accessibility =
                            "Rating: 8/10\r\nPhysical Access: The market is located on ground level with an open, spacious layout, making it accessible for those exploring on foot or via the adjacent Ciclovía.\r\nNavigation: While generally easy to move through, the indoor floors can be wet and salty from the ice and fresh catch.\r\nFacilities: Note that the bathroom facilities are very basic, which may be a consideration for families or those with specific needs.\r\nSafe Route: For those coming from Casco Viejo, the \"waterfront boardwalk\" route under the highway is noted as being safer and more pleasant than attempting to cross the main road.\r\n",
                        IdealDuration =
                            "45 Minutes (Quick snack) to 1.5 Hours (Full lunch/exploration)",
                    },
                    BestPeriodToVisit = new List<TravelPeriod> { TravelPeriod.YearRound },
                    MaxCost = 15,
                    MinCost = 5,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = panamaCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = mercadoCatIds,
                    TagIds = mercadoTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Biomuseo",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/biomuseo.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "If architecture is the art of frozen music, the Biomuseo is a technicolor jazz symphony. Designed by Frank Gehry—the visionary behind the Guggenheim in Bilbao—it is his only work in Latin America.\r\nWhile the outside is a chaotic, vibrant splash of folded metal intended to reflect the lush biodiversity of Panama, the inside tells a much more structured story: how a narrow strip of land rose from the sea, bridged two continents, and fundamentally changed the world’s climate and evolution forever.\r\n",
                        Directions =
                            "connecting the mainland to four islands.\r\nGetting There:\r\nBy Car/Uber: A 10–15 minute ride from downtown Panama City. Uber is highly reliable and usually costs under $10 from the city center.\r\nPublic Transit: Take Bus Route C850 from the Albrook Metro Station (Line 1). The bus stops directly at the museum and continues along the Causeway.\r\n",
                        WhatToKnow =
                            "Architecture with a Purpose: The building’s chaotic, colorful roof isn't just for show; it was designed by Frank Gehry to represent the diverse cultures and tropical flora of Panama. It is his only project in all of Latin America.\r\nThe \"Panamarama\" Experience: One of the most famous parts of the museum is a three-level projection space that immerses you in Panama's various ecosystems through sights and sounds.\r\nLimited Hours: The museum closes relatively early compared to other attractions (3:00 PM daily). Arriving late in the afternoon may result in a rushed experience.\r\nThe Outdoor \"Museum\": Your experience continues outside in the Biodiversity Park. This area is free to roam and offers some of the best views of ships passing under the Bridge of the Americas.\r\nEducation Focus: The exhibits are highly interactive, making it a \"culinary and cultural delight\" for both adults and children, though the focus is heavily on biology and geology.\r\nPro Tip: If you're into photography, head to the back of the Biodiversity Park near the water's edge. You'll get a shot that frames the neon colors of the museum in the foreground with the massive container ships and the Bridge of the Americas in the background. It’s the ultimate \"Panama\" shot.\r\n",
                        ThingsToBeWaryOf =
                            "Afternoon Heat: The museum's interior is air-conditioned, but the outdoor Biodiversity Park and the walk along the Causeway are very exposed. If you plan to explore the grounds, do it first or last to avoid the midday sun.\r\nEarly Closing: With a 3:00 PM closing time, an \"afternoon visit\" can feel rushed. Aim to arrive no later than 1:00 PM to truly see all eight galleries.\r\nWindy Conditions: Being on a causeway means it can get very windy. If you’re wearing a hat or planning a picnic in the park, be prepared for some heavy Pacific gusts.\r\n",
                        LocalPerspective =
                            "The \"Bridge of Life\": The museum isn't just about plants and animals; it’s about geology. The central theme is the formation of the Isthmus of Panama. Don't miss the \"Panamarama\" gallery—a three-level projection space that makes you feel like you are standing inside a living ecosystem.\r\nThe Gehry Signature: Look up at the roof. The fragmented, colorful panels are classic Gehry, but the colors themselves are local inspirations: the yellow, red, and blue represent the tropical flora and the diverse cultural heritage of Panama.\r\nThe Biodiversity Park: The experience doesn't end at the exit. The surrounding park features gardens like the \"Garden of Evolution\" and \"Crops Garden,\" which offer some of the best unobstructed views of the Bridge of the Americas and ships entering the Panama Canal.\r\n",
                        HiddenCost =
                            "Non-Resident Pricing: Be aware that \"starting from $20\" is the baseline for international adults. Families should check for 4-person package deals ($60 for non-residents) which can save you a bit of money.\r\nParking: While parking is available, it can be limited during weekend events on the Causeway.\r\nCopa Airlines Promo: If you are flying with Copa and have a stopover, check for the \"BIOMUSEOCOPA\" promo which can drop your ticket price significantly (down to ~$12).\r\n",
                        NearbyComplements =
                        [
                            "Punta Culebra Nature Center: A short 5-minute drive further down the Causeway. It's run by the Smithsonian Tropical Research Institute and features touch tanks and a chance to see sloths in the trees.",
                            "The Causeway Walkway: After the museum, rent a bicycle or a four-wheeled pedal cart to explore the rest of the islands.",
                            "Taboga Ferry: The terminal for ferries to the \"Island of Flowers\" is located just minutes away, making the museum a perfect morning stop before a beach afternoon.",
                        ],
                        BestTimeToVisit =
                            "The \"Sweet Spot\": Wednesday or Thursday morning at 10:00 AM. You beat the weekend school groups and the heat, and the light is perfect for photographing the building's exterior.\r\nSeasonally: The Dry Season (December–April) is best if you want to enjoy the outdoor Biodiversity Park without a tropical downpour.\r\nHours: * Tuesday–Friday: 9:00 AM – 3:00 PM\r\nSaturday–Sunday: 10:00 AM – 3:00 PM (Note: The museum closes relatively early).\r\n",
                        crowdLevel =
                            "Moderate (6/10): As a premier architectural and educational landmark, it maintains a steady flow of visitors.\r\nPeak Times: Saturdays and Sundays are the busiest days, often attracting local families and school groups.\r\nQuiet Windows: Visiting on a weekday morning (Tuesday through Friday) shortly after the 9:00 AM opening is the best way to have the galleries to yourself.\r\n",
                        Accessibility =
                            "Rating: 10/10\r\nModern Infrastructure: Because this is a modern, world-class facility, it is fully accessible to all visitors. It features level flooring, wide corridors, and elevators to reach the different viewing tiers.\r\nGuided Layout: The eight galleries are organized in a clear, logical flow that is easy to navigate with wheelchairs or strollers.\r\nOutdoor Park: The surrounding Biodiversity Park also features paved paths, allowing for a seamless transition from the indoor exhibits to the outdoor botanical areas.\r\n",
                        IdealDuration =
                            "1.5 to 2 Hours: This is the standard time required to explore all eight indoor galleries, including the \"Panamarama\" projection and the \"Oceans Divided\" aquarium.\r\nExtended Visit: If you plan to spend time in the Biodiversity Park or take photos of the unique exterior architecture from the various garden lookouts, allow for a full 2.5 hours.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod> { TravelPeriod.YearRound },
                    MaxCost = 10,
                    MinCost = 20,
                    SafetyLevel = 9,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = panamaCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = museoCatIds,
                    TagIds = museoTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Bocas Town (Isla Colón)",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/bocastown.webp",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Bocas Town is the vibrant, kaleidoscopic heart of the archipelago. It is a place where the air smells of grilled meat and salt spray, where sidewalks are more of a suggestion than a rule, and where the transition from a hardware store to a sushi bar is just a few steps away. It’s rustic, unpretentious, and possesses a \"low-key\" energy that makes it easy to lose track of time\r\nBocas Town is the kind of place where you might go in for a box of Alieve and come out with two pills and a newfound appreciation for sushi. It’s a town that demands you slow down and accept its \"spirited\" rhythm.\r\n",
                        Directions =
                            "Location: The southern tip of Isla Colón.\r\nArrival: Most travelers arrive via Bocas del Toro \"Isla Colón\" International Airport (daily flights from Panama City) or by water taxi from the mainland.\r\nNavigation: The town is a simple grid. Avenidas run east-to-west, and Calles run north-to-south. It is small enough that most points of interest are within a 10-minute walk.\r\n",
                        WhatToKnow =
                            "Panga Safety: Water taxis are a \"spirited\" experience. Drivers often go full-speed over waves; sit in the back of the boat to minimize the jarring impact on your back and avoid getting soaked.\r\nIntense Heat: The tropical sun is \"no joke.\" Many hardware and grocery stores are not air-conditioned and can become \"hot, hot, hot\" inside. Seek out Super Gourmet if you need a blast of cool air while you shop.\r\nIsland Time: Service in stores can be \"terribly frustrating\" for those used to efficiency, as clerks often wait on multiple customers simultaneously.\r\n",
                        ThingsToBeWaryOf =
                            "The \"Spirited\" Panga Rides: Water taxis (pangas) often have two speeds: fast and stop. These rides can be \"back-jarring\" and wet. Pro Tip: Sit toward the back of the boat for a smoother ride, and always keep your electronics in a dry bag.\r\nThe Sun: The tropical intensity here is no joke. The heat can be \"brutally hot,\" especially inside the packed hardware or grocery stores.\r\nSidewalk Gaps: Sidewalks begin and end abruptly. Most people simply walk in the street alongside bicycles and the occasional \"clown-car\" work van.\r\n",
                        LocalPerspective =
                            "Shopping Strategy: Locals don't expect one-stop shopping. You will likely visit several of the Chinese-run grocery stores (like Isla Colon or Christina’s) to find everything on your list, as stock and fresh deliveries vary by location.\r\nIndividual Unit Buying: It is perfectly normal to buy single items rather than packs—you can purchase just two or three pills from a pharmacy or a single slice of cheese.\r\nThe Happy Hour Work-Around: Many long-term visitors combine their grocery runs with early happy hours. Don't be shy about bringing your backpacks and heavy grocery bags into a restaurant for a drink to escape the heat.\r\n",
                        HiddenCost =
                            "Water Taxis: While the town is walkable, reaching beaches like Red Frog or neighboring islands requires a panga. These costs add up, especially if you are timing your trips around fixed marina schedules.\r\nImported Goods: Stores like Super Gourmet carry US-brand luxuries (like Philly cream cheese or decent bacon), but you will pay a significant premium for the air-conditioned shopping experience.\r\nHardware Frustration: Service in hardware stores can be slow, with clerks often helping multiple customers at once. Factor extra \"patience time\" into your errands.\r\n",
                        NearbyComplements =
                        [
                            "Red Frog Beach: A short panga ride away on Bastimentos Island. It offers a lush jungle backdrop, hiking trails, and caves with bats.",
                            "The Pub & Toro Loco: Popular waterfront hangouts for expats and travelers to catch a breeze and an affordable margarita.",
                            "Bastimentos Town (Old Bank): A more traditional, less developed Afro-Caribbean village just across the water.",
                        ],
                        BestTimeToVisit =
                            "For Weather: October, January, and March are statistically the driest months in this tropical rainforest climate.\r\nDaily Timing: Morning is best for errands before the midday heat becomes \"bloody hot.\" Late evening (after 8:00 PM) is when the town truly comes alive with music and the smell of street food.\r\n",
                        crowdLevel =
                            "High (8/10)\r\nBustling Hub: As the provincial capital and the primary \"tourist resort\" of the archipelago, it is a high-energy environment filled with \"people of all types\".\r\nPeak Activity: The town is most crowded during the midday \"panga\" (water taxi) departures and after 8:00 PM when the waterfront restaurants and nightlife venues fill up.\r\n",
                        Accessibility =
                            "Rating: 9/10\r\nPaved Infrastructure: This is the only island in the archipelago with paved roads, making it the most accessible for those with mobility needs or those traveling by bicycle.\r\nWalkability: The town is designed on a compact grid (Avenues running East-West, Streets North-South), and most essential services are within easy walking distance.\r\nNote on Sidewalks: While the roads are paved, sidewalks are inconsistent and often \"begin and end\" abruptly, requiring you to walk in the street with local traffic.\r\n",
                        IdealDuration =
                            "3–5 Days\r\nThis allows enough time to handle the slow pace of \"island time\" errands, explore the town's international food scene, and take day trips to neighboring islands like Bastimentos.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                    },
                    MaxCost = 50,
                    MinCost = 5,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = cariCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = bocasCatIds,
                    TagIds = bocasTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Red Frog Beach",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/redfrog.jpeg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Red Frog Beach is a striking balance between wild Caribbean beauty and resort-style comfort. Named for the Strawberry Poison Dart Frogs that inhabit the island, it offers a more secluded feel than the main town while still providing high-end amenities like spa treatments and villas.\r\nRed Frog Beach is a vivid sensory experience where the dense, emerald canopy of Isla Bastimentos meets the powerful turquoise of the Caribbean. It offers a distinct \"wild\" feeling compared to other beaches in the archipelago, characterized by its shifting tides, lush soundscape, and the iconic wildlife for which it is named.\r\nDirection:\r\nUsually reachable by taxi, tour, ferry, or boat depending on destination.",
                        Directions =
                            "Option 1: The Jungle Shortcut (Public Access)\r\nThis is the most common route for those looking for a rustic experience.\r\nBoat Trip: Take a 15-minute water taxi from the docks in Bocas Town to the \"Red Frog Marina\" or the public \"shortcut\" entrance on the leeward side of the island.\r\nThe Tariff: Upon arrival, you will pass a small shack where a $5 USD per person fee is collected to cross the private land and national forest area.\r\nThe Hike: Follow the well-maintained, groomed jungle pathway. It is a roughly 10-minute walk through the rainforest before the trees open up to the beach.\r\nOption 2: Resort Drop-off\r\nSpecify to your boat captain that you want to be dropped off at the Red Frog Beach Island ResortClick to open side panel for more information landing.\r\nPurchase the $45 USD day pass at the registration area (this serves as a credit for food and drinks).\r\nUse the resort's golf cart shuttle or follow the paved paths directly from the dock to the beach.\r\n",
                        WhatToKnow =
                            "Strong Surf: Be wary of strong rip tides. While a sandbar 10 meters offshore creates a calmer area, the sea here is generally rougher than other Bocas beaches—great for body surfing, but use caution when swimming.\r\nHeat Warning: The sand gets extremely hot. Bring flip-flops or water shoes for the walk from the jungle to the water’s edge.\r\nDrop-off Choice: You must specify your drop-off point to the boat captain. Choose the \"shortcut\" for a rustic beach day or the \"resort\" for pool access and credits.\r\nThe \"Short Cut\" Vibe: This is the authentic, adventurous route. After paying your $5 tariff at the shack, you walk a groomed, 10-minute path through the National Forest. It feels like a miniature expedition, where you might spot a sloth hanging above the trail before the trees suddenly open up to the ocean.\r\nThe Resort Vibe: The Red Frog Beach Island Resort anchors the area with a more manicured, \"upscale managed\" feel. It transforms the wild beach into a luxury destination with villas, a hilltop pool, and a jacuzzi. The $45 day pass allows you to toggle between the raw power of the ocean and the comfort of a poolside bar with gold cart service.\r\n",
                        ThingsToBeWaryOf =
                            "Strong Rip Tides & Rough Surf: The sea here is known to be rough, with powerful Caribbean waves. While there is a sandbar about 10 meters offshore that creates a calmer pocket, the rip tides can be dangerous. It is vital to swim only in designated or calm areas.\r\nExtremely Hot Sand: The white sand at Red Frog absorbs an intense amount of heat. Walking barefoot from the jungle path to the water can result in burns; water shoes or flip-flops are essential.\r\nThe \"Spirited\" Boat Ride: The 15-minute panga ride from Bocas Town through the mangroves is often very bumpy. Drivers tend to operate at high speeds, which can be jarring for your back and may result in you getting soaked by spray.\r\nLimited Natural Shade: While the jungle meets the sand in some \"alcoves,\" much of the main beach is highly exposed. If you aren't at a restaurant or the resort, you may struggle to find relief from the sun.\r\nHigh Tide Shoreline: During high tide, the sea creeps high enough to \"swallow\" much of the walkable shoreline, which can leave you with very little space to relax if you haven't secured a spot further back.\r\n",
                        LocalPerspective =
                            "The \"Secret\" Beach: Instead of stopping at the main beach, turn LEFT away from the sea when you reach the end of the houses. This road leads to a \"hermit shack\" and a much more spectacular, deserted beach that is even better for spotting frogs.\r\nFrog Spotting: You don’t need a guide to see the red frogs; they are typically found in the leaves along the jungle path and near the wetland areas behind the beach.\r\nThe \"Back of the Boat\" Rule: Just like the ride to Bocas Town, sit toward the back of the panga. The 15-minute ride through the mangroves can be \"spirited\" and bumpy.\r\n",
                        HiddenCost =
                            "The \"Shortcut\" Tariff ($5): If you take the standard water taxi to the \"short cut\" drop-off, a man in a shack will collect a $5 per person fee to allow you to walk through the National Forest to reach the beach.\r\nThe Resort Day Pass ($45): If you prefer to be dropped off at the Resort to avoid the 10-minute jungle walk, you must pay a $45 day pass fee. While this is credited back to you for food and drinks, it is a significant upfront cost.\r\nEquipment Rentals: Unlike some public beaches, chairs and towels are not usually free. You will likely need to rent them from the bar/restaurant next to the Palmar Beach Lodge if you aren't a guest.\r\nHigh-Priced Small Items: Expect \"island pricing\" for convenience items. For example, two smoothies can cost $15 with tip, so small snacks and drinks add up quickly if you don't bring your own.\r\nWater Taxi Returns: Remember that water taxi fees are usually per person, per way. If you miss a scheduled marina panga, you may have to pay a premium for a private launch to get back to Bocas Town.\r\n",
                        NearbyComplements =
                        [
                            "Nature Trails: Beyond the sand, the island features inland hiking trails and even caves inhabited by bats, offering a break from the sun for those interested in a more active \"jungle trek.\"",
                            "The Mangrove Transit: Even the journey there is part of the description—a 15-minute panga ride that weaves through the intricate Caribbean mangroves, showcasing the unique coastal ecology of Panama.",
                        ],
                        BestTimeToVisit =
                            "Daily Timing: Visit in the morning to increase your chances of seeing wildlife like sloths and frogs before they retreat from the midday heat.\r\nSeasonally: The resort is a \"hurricane-free zone\" with a consistent 27°C (82°F) climate, but visiting during the dryer months (Oct, Jan, March) ensures the jungle paths are less muddy.\r\n",
                        crowdLevel =
                            "Moderate (5/10)\r\nBecause of the entry fee and boat ride, it is quieter than beaches on Isla Colón. It offers \"secluded alcoves\" and private spots, though the area near the beach bars can get lively during peak season.\r\n",
                        Accessibility =
                            "Ratings: 7/10\r\nThe main beach is accessed via a 10-minute walk along a groomed, well-maintained jungle boardwalk. While the path is clear, it is a trek through the forest.\r\nFor those with limited mobility, the Red Frog Beach Island Resort drop-off is the better choice, as they provide golf cart transportation around the property.\r\n",
                        IdealDuration =
                            "2 Hours to a Full Day\r\n2 hours is sufficient for a quick swim and frog spotting, but the resort and various trails can easily fill an entire day.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                    },
                    MaxCost = 190,
                    MinCost = 5,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = cariCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = redCatIds,
                    TagIds = redTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Starfish Beach (Bocas del Toro)",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/starfishbeach.jpeg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Starfish Beach (Playa Estrella) is the postcard-perfect side of Isla Colón. While other beaches in the archipelago are known for powerful surf, this is a \"still and calm\" haven where the water is the perfect temperature for sitting in up to your neck. It’s a place of clear, turquoise water, golden sand, and a quirky population of giant orange cushion starfish.",
                        Directions =
                            "From Bocas Town: Head to the main park (Parque Simón Bolívar).\r\nThe Bus (Colectivo): Look for the white minibuses marked \"Boca del Drago.\" The fare is approximately $2.50–$3.00 USD each way, and the trip takes about 45 minutes.\r\nThe Trail: The bus deposits you at Boca del Drago. From there, follow the coastal path to the left. It is a 1.5 km (15-20 minute) walk that is mostly flat and winds through the trees along the water's edge.\r\nAlternative: You can hire a boat/water taxi directly from the docks in Bocas Town to skip the bus and walk, though this is significantly more expensive.\r\n",
                        WhatToKnow =
                            "Starfish Protection: This is non-negotiable—do not touch or remove the starfish from the water. They suffocate and die quickly when exposed to air. There are signs everywhere, but they are often ignored by tourists; be a responsible traveler and admire them from above the surface.\r\nThe \"Shack\" Scene: The \"restaurants\" here are mostly basic shacks with plastic chairs. They are unpretentious but serve great local seafood and cold Balboa beers.\r\nThe Trail Walk: The 15-20 minute walk from the bus drop-off at Boca del Drago is a \"stunner.\" The water comes right up to the trees, and the path is flat and well-defined, though it can get muddy after rain.\r\n",
                        ThingsToBeWaryOf =
                            "\"Bitey Little Shits\": The sand is inhabited by sandflies or sand fleas. They are nearly invisible but can leave itchy red welts that \"read like braille\" on your skin. They are most active at dusk or in the shade; staying in the water is the best defense.\r\nCoconut Hazards: Do not settle directly under palm trees laden with fruit. Coconuts can fall unexpectedly with enough force to \"brain you\".\r\nStarfish Protocol: It is strictly forbidden to touch or lift the starfish out of the water. They breathe through their skin and will suffocate within seconds of exposure to air.\r\nQuick Drop-offs: While the water is generally shallow, it can \"drop off quite quickly and deeply\" in certain sections further from the shore.\r\n",
                        LocalPerspective =
                            "The \"Bitey Little Shits\": The sand is home to sandflies or sand fleas. They are invisible but leave \"tiny red welts\" that can feel like braille on your skin. Pro-Tip: The bugs can't get you in the water. Stay submerged or keep a beer in hand while sitting neck-deep in the sea to stay bite-free.\r\nThe Coconut Hazard: Never settle directly underneath a palm tree laden with coconuts. They can fall with an \"almighty crack\" and cause serious injury. Always inspect the canopy before laying down your sarong.\r\nEat Your Catch: If you go on a fishing charter, many of the rustic shacks at Playa Estrella will cook your fresh catch (like tuna) for you while you wait. It’s a common practice that turns a lucky catch into a beachfront feast.\r\n",
                        HiddenCost =
                            "Transportation: The colectivo (minibus) from Bocas Town is cheap ($2.50–$3.00), but if you opt for a private water taxi directly to the beach, the price will be significantly higher.\r\nAmenities: Expect to pay about $5 for a shaded beach chair. Since the best shade is often deep under the trees (see \"Coconut Hazard\"), many people find the $5 rental fee worth it for safe, nut-free cover.\r\nCash Only: Most vendors and the bus drivers do not accept cards. Bring enough small bills for the bus, beer, and lunch.\r\n",
                        NearbyComplements =
                        [
                            "Boca del Drago: This is the entry point where the bus drops you off. It is quieter than Starfish Beach and offers a more rugged coastal feel with a few local seafood restaurants.",
                            "Bird Island (Isla Pájaros): A stunning rock formation and bird sanctuary visible from the coast. Many boat tours from Bocas Town combine a visit to Starfish Beach with a stop here for snorkeling and sightseeing.",
                            "Bocas Town: After your beach day, most visitors head back to the main town for a wider variety of dining. JJ’s is a local favorite known specifically for having the \"best buffalo wings on the island\" and high-quality burgers.",
                            "The Fire Station (Bocas Town): For a quick bit of local history, you can visit the fire station to see their collection of antique fire engines, some nearly 100 years old.",
                        ],
                        BestTimeToVisit =
                            "Daily Timing: Early morning (before 10:00 AM) is the \"sweet spot.\" You’ll beat the afternoon crowds, find more starfish in the shallows, and secure a spot before the heat becomes intense.\r\nWeekly Timing: Stick to weekdays. Weekends are popular with local families and large tour groups, which can make the narrow beach feel cramped.\r\n",
                        crowdLevel =
                            "High (8/10)\r\nWeekend Peak: This is one of the most popular spots in the archipelago. It becomes \"very busy\" on weekends when local families and large tour groups arrive.\r\nThe Quiet Window: To enjoy the \"paradise\" feel without the crowds, you must arrive early in the morning (before 10:00 AM) and stick to weekdays.\r\n",
                        Accessibility =
                            "Rating: 7/10\r\nThe Trail: Once the bus drops you at Boca del Drago, the path to the beach is a 1.5 km (15-20 minute) walk. The trail is flat and well-defined, winding beautifully along the water’s edge.\r\nMobility Considerations: While the path is flat, it can become muddy after rain. For those who wish to skip the walk entirely, water taxis can be hired to take you directly from Boca del Drago or Bocas Town to the sand.\r\nOn the Beach: The beach itself is narrow, but once you are there, everything is concentrated in a small, walkable area.\r\n",
                        IdealDuration =
                            "4 to 6 Hours\r\nTravel Time: You must factor in the 45-minute bus ride each way from Bocas Town, plus the 20-minute walk.\r\nThe Experience: This duration allows you enough time to walk the scenic trail, snorkel with the starfish in the shallows, and have a relaxed lunch at one of the rustic beach shacks.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                    },
                    MaxCost = 10,
                    MinCost = 5,
                    SafetyLevel = 8,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = cariCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = starfishCatIds,
                    TagIds = starfishTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Wizard Beach",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/wizardbeach.jpeg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Wizard Beach (Playa Primera) is the raw, untamed soul of Isla Bastimentos. Unlike the manicured resort stretches of the archipelago, Wizard is a vast, golden expanse backed by dense jungle, known for its \"pure disconnect\" energy. It is a place of high reward but high risk, demanding physical effort to reach and constant vigilance once you arrive.",
                        Directions =
                            "Water Taxi: Take a boat from Bocas Town to the main dock at Old Bank on Isla Bastimentos.\r\nThe Trailhead: Walk into the village; the path is often located behind local houses (if you feel like you're in someone's backyard, you're likely on the right track).\r\nThe Hike: Follow the signs for \"Wizard Beach\" or \"Playa Primera.\" Stay on the well-defined but slippery path until it opens up to the Pacific.\r\n",
                        WhatToKnow =
                            "The \"Insane\" Hike: The trail from Old Bank takes 30–45 minutes. It is notoriously steep and muddy, even if it hasn't rained recently. Sturdy shoes are mandatory—flip-flops will be destroyed or lost in the mud.\r\nLethal Rip Tides: The \"suction\" of the current directly in front of the trail entrance is life-threatening. Only swim in the designated areas indicated by local military or rangers (usually further to the left or right of the entrance) and never go deeper than your hip.\r\nZero Amenities: This is a wild beach. There are no restaurants, no bathrooms, and no shade structures. You must carry in every drop of water and every snack you need.\r\nPro-Tip: If you absolutely must visit, leave all valuables (phones, cards, extra cash) at your hotel. Take only the cash needed for your water taxi and carry it in a waterproof pouch that stays on your person even while you swim.\r\n",
                        ThingsToBeWaryOf =
                            "Targeted Theft: This is the most significant concern. Thieves are known to hide in the jungle treeline and watch tourists. If you leave your bags unattended while swimming, they may be snatched within minutes. You risk losing everything, including your clothes and shoes.\r\nLethal Currents: The rip tides here are life-threatening. The \"suction\" is particularly strong directly in front of the trail entrance.\r\nSafety Rule: Follow the instructions of any uniformed rangers or military on-site. Only enter the water to your left or right of the entrance, and never go deeper than your hips.\r\nWildlife & Trail Conditions: The jungle path can be slippery and home to local wildlife. Ensure you have plenty of daylight for the return hike to avoid getting lost or injured on the muddy slopes.\r\n",
                        LocalPerspective =
                            "The \"Bush Watchers\": Local thieves are known to watch tourists from the dense jungle treeline. They wait for solo travelers or couples to enter the water together, then \"jump out of the bushes,\" grab bags, and disappear into the endless jungle.\r\nThe Underwear Risk: Theft here is often \"total\"—thieves take everything, including clothes and shoes, which can leave you stranded in your swimwear with a 45-minute muddy hike back to civilization.\r\nGPS Futility: While your phone's GPS might show the location of stolen goods in a nearby village, local police are notoriously slow to act on these specific cases; prevention is the only real protection.\r\n",
                        HiddenCost =
                            "The Price of Mud: Many travelers end up discarding their shoes after the hike because the jungle mud is so pervasive and difficult to clean.\r\nThe \"Solo Traveler\" Tax: If you are solo, you cannot safely swim. You must pay with your time by staying with your bags at all times, or risk losing everything.\r\n",
                        NearbyComplements =
                        [
                            "Old Bank: The colorful, Afro-Caribbean village where your hike begins. It’s a great place to grab a cold drink or a local meal after the strenuous return hike.",
                            "Up in the Hill: A popular organic cacao farm and coffee shop located along the paths of Isla Bastimentos, often visited by those looking for a jungle experience without the full intensity of the Wizard Beach surf",
                            "Red Frog Beach: Accessible via a different trail or boat ride. While it also has security concerns, it offers more amenities like restaurants and a resort environment if you want a more \"managed\" beach day.",
                            "Bocas Town: Since Wizard Beach has no food or water, you'll likely want to head back to the main island for dinner. JJ's is highly recommended for its burgers and wings to refuel after the hike.",
                        ],
                        BestTimeToVisit =
                            "For Surfers: December to March and June to August provide consistent 3–10 ft waves. January is the peak month for \"clean\" rideable swells.\r\nFor Swimming: April to June and September to October. During these months, the Caribbean calms down, reducing the intensity of the rip tides and making the water safer for bathing.\r\n",
                        crowdLevel =
                            "Very Low (2/10)\r\nThe Reward: Because of the difficult access, you will often find the beach nearly deserted. If you go early, it is common to have the entire stretch of yellow sand to yourself.\r\n",
                        Accessibility =
                            "Rating: 3/10\r\nThe Hike: This beach is reached via a 30–45 minute jungle hike from Old Bank. The trail is described as \"insane,\" exceptionally steep, and frequently deep with mud.\r\nPhysical Demand: It requires high physical effort and sturdy footwear; it is not suitable for those with limited mobility or for walking in flip-flops.\r\n",
                        IdealDuration =
                            "2 to 3 Hours\r\nBecause there are zero amenities and the safety risks are higher, most visitors find that a few hours of enjoying the views and a quick dip (staying shallow) is sufficient. It is not recommended to stay until dusk when the trail becomes harder to navigate and security risks increase.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod> { TravelPeriod.YearRound },
                    MaxCost = 15,
                    MinCost = 10,
                    SafetyLevel = 4,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = cariCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = wizardCatIds,
                    TagIds = wizardTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Cayos Zapatilla",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/cayos.jpeg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Cayos Zapatilla is the \"postcard\" destination of Bocas del Toro—a pair of uninhabited, pristine islands within the Bastimentos Island National Marine Park. Recognized as a UNESCO World Heritage site, these islands offer white \"powder\" sands and some of the healthiest coral gardens in the region.",
                        Directions =
                            "Departure: Tours and private launches typically leave from the docks in Bocas Town (Isla Colón) between 9:00 AM and 10:00 AM.\r\nTransit: The boat journey takes approximately 1.5 hours each way, usually passing through the Bastimentos Island National Marine Park.\r\nThe Route: Most tours make \"stepping stone\" stops:\r\nFirst: Sloth Island (viewing from the boat).\r\nSecond: Cayo Coral (30–45 minutes of snorkeling).\r\nThird: Cayos Zapatilla (The main landing).\r\nBooking: You can find tour operators near the Rush nightclub or arrange a private boat through your hotel to leave earlier and beat the 10:00 AM rush.\r\n",
                        WhatToKnow =
                            "Wet Landing: Since there are no docks, boats drop you in shallow water. Wear waterproof gear or carry your electronics/bags above your head as you walk to the beach.\r\nThe Interpretive Trail: Don't just stay on the beach. There is a well-maintained path called El Bosque Detrás del Arrecife (\"The Forest Behind the Reef\") that circles the eastern island in under an hour, offering views of mangroves and coconut plantations.\r\nChitras (Sand Flies): These tiny biters can be present on the beaches. If you plan to stay late or camp, bring repellent to avoid itchy welts.\r\n",
                        ThingsToBeWaryOf =
                            "The \"Lunch Trap\": Standard tours stop at a restaurant at noon to take orders, but you won't actually eat until 3:30 PM after the island visit. The food is expensive, and the wait is long. Pack your own lunch.\r\nChitras (Sand Flies): These tiny biting insects can be prevalent on the beaches. They are most active at dawn, dusk, or in still air.\r\nSun Intensity: Because the islands are pristine and white-sand, the sun reflection is intense. There is limited shade near the water landing.\r\n",
                        LocalPerspective =
                            "The Lunch Strategy: Standard tours stop at a restaurant in Cayo Coral around noon to take orders, but you won't actually eat until 3:30 PM. The food at these stops is often overpriced. Pro-Tip: Pack your own lunch and snacks so you can eat on the island and avoid the \"tourist trap\" pricing and late meal times.\r\nThe \"First on the Island\" Trick: Most group tours leave Bocas Town at 10:00 AM. If you arrange a private boat (like with Junior from Bocas Islands Adventurers) to leave earlier, you can experience the island in \"unforgettable\" silence before the mass of tour boats arrive.\r\nWildlife Encounters: While snorkeling, look for nurse sharks and moray eels in the underwater caves. On land, keep your eyes on the shoreline; these islands are critical nesting sites for the critically endangered Hawksbill sea turtles.\r\n",
                        HiddenCost =
                            "Park Entrance Fee: Because the islands are part of a National Marine Park, you must pay an entrance fee at the ANAM ranger station on the eastern island (keep small cash ready).\r\nSnorkel Gear: Most organized tours provide gear, but if you hire a private water taxi, you may need to rent your own masks and fins in Bocas Town beforehand.\r\n",
                        NearbyComplements =
                        [
                            "Sloth Island: A mangrove island usually visited on the way to Zapatilla where you can spot sloths from the boat.",
                            "Cayo Coral: The premier snorkeling stop nearby with vibrant coral beds and tropical fish.",
                            "Hollywood / Starfish Island: A mangrove-surrounded area where you can see starfish in the seagrass, often the final stop on the return trip to town.",
                        ],
                        BestTimeToVisit =
                            "Time of Year: September and October are often considered the best for Zapatilla because the sea is at its calmest, leading to the best visibility for the coral gardens which can otherwise be \"cloudy or mediocre\".\r\nDaily Timing: Aim to land by 9:00 AM if going private, or book your tour a day in advance for a 9:00–10:00 AM departure.\r\n",
                        crowdLevel =
                            "Moderate (6/10)\r\nPeak Windows: As one of the most popular destinations in Bocas, it sees a steady stream of tour boats between 11:00 AM and 3:00 PM.\r\nFinding Solitude: Because the islands are uninhabited and large (35–84 acres), you can easily \"walk to parts of the island where there is no one in sight\" even on a busy day.\r\n",
                        Accessibility =
                            "Rating: 4/10\r\nThe \"Wet Landing\": There are no docks. Boats typically drop you in waist-deep water, and you must wade to the shore. This requires a moderate level of physical mobility and waterproof bags for your belongings.\r\nIsland Terrain: Once on land, accessibility is high. The interpretive trail (El Bosque Detrás del Arrecife) is in \"very good condition\" and takes less than an hour to walk.\r\nRemote Location: The 1.5-hour boat ride can be bumpy; it is not recommended for those highly prone to seasickness or significant back issues.\r\n",
                        IdealDuration =
                            "Island Time: Most visitors spend 2–3 hours on the island itself, which is plenty of time to lounge on the white sand and walk the entire perimeter.\r\nFull Experience: If you include the transit (1.5 hours each way) and nearby snorkeling stops like Cayo Coral, the total trip duration is usually a full day (approx. 6–7 hours).\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 100,
                    MinCost = 30,
                    SafetyLevel = 8,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = cariCityIds,
                    LanguageIds = panamaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = zapaCatIds,
                    TagIds = zapaTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "San Blas Islands (Via Carti)",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/carti.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "I’ve often noticed that some of the most beautiful destinations require a bit of a \"tax\" on your comfort.\r\nFor the islands of the Guna Yala, that tax is paid on the El Llano-Cartí Road. It is a 30-kilometer ribbon of asphalt that winds through the primary rainforest like a rollercoaster. One moment you are looking at the canopy of the Darién Gap, and the next you are navigating \"craters\" that seem designed to test the suspension of even the toughest 4x4.\r\nBut as the mountains fall away and the smell of salt air replaces the humid scent of the jungle, you reach the port. It isn't a polished marina with yachts and boutiques; it’s a bustling, rustic gateway where the rules of the city stop and the traditions of the Guna begin.\r\nWe often think of travel as a seamless line from A to B. But Cartí is a reminder that the transition—the bumpy road, the passport check, the chaotic loading of a small boat—is what makes the arrival feel earned. It is the moment you realize you aren't just in a different province; you’ve crossed into an autonomous world that moves at the speed of the tide.\r\n",
                        Directions =
                            "The Drive: From Panama City, take the Panamericana to the El Llano-Carti Road.\r\nVehicle Requirements: A 4x4 or all-wheel-drive vehicle is required for the steep sections, mountainous curves, and the final unpaved portion near the port.\r\nNavigation: It is recommended to use Waze instead of Google Maps, as it handles the local roads and winding mountain turns more effectively.\r\nKey Stops: Turn off the main highway at the Texaco gas station to start the mountain drive. About 30 minutes into the mountain road, you must stop at a checkpoint to present passports and pay a $20 fee for foreign visitors.\r\nThe Port: The final five minutes of the drive are unpaved and can be chaotic; once at the gate, you must pay a small access fee ($2 per person and $3 per car) to proceed to the numbered ports.\r\n",
                        WhatToKnow =
                            "Bring cash (Quarters and Dollars): The port is a cash-only ecosystem. Small bills and quarters are essential for the $0.50 bathroom fees and small tips.\r\n4x4 is Mandatory: The road is steep, winding, and heavily deteriorated. Only high-clearance 4x4 or AWD vehicles are allowed past the checkpoint.\r\nPassport Required: You are entering an autonomous indigenous territory. You must present your physical passport at the checkpoint (about 30 minutes before the port).\r\nThe Texaco Stop: The Texaco gas station at the El Llano turnoff is the last place for snacks, ice, and a \"real\" bathroom before the mountain crossing.\r\nPack Light: Use backpacks instead of hard suitcases. Boat shuttles are small, and space is limited.\r\nWaze over Google: Waze handles the winding mountain turns and local landmarks more accurately in this region.\r\nDress for the Boat: Wear flip-flops and clothes that can get wet from sea spray during the lancha transfer.\r\n",
                        ThingsToBeWaryOf =
                            "Road Conditions: The 40km stretch is full of deep potholes and steep inclines. Drive with extreme caution.\r\nMotion Sickness: The \"rollercoaster\" nature of the mountain road is notorious for making passengers ill. Take Dramamine before leaving the highway.\r\nThe \"Chaotic\" Arrival: The final unpaved stretch to the port can feel overwhelming. Look for the port number (#1, #2, or #3) assigned to your tour operator.\r\nLimited Infrastructure: Facilities at the port are basic. Don't expect high-end waiting areas or Wi-Fi.\r\n",
                        LocalPerspective =
                            "The \"Border\" Mindset: For the Guna, the checkpoint isn't just a toll—it’s a symbol of the 1925 Revolution. When you show your passport, you are acknowledging their hard-won political autonomy.\r\nThe Matriarchal Grip: Tourists often see men driving the boats, but locals know that the women own the houses and the canoes (cayucos). Decisions about land use and port fees are often discussed in community congresses where women’s voices carry immense weight.\r\nThe \"Omeggid\": You may encounter Guna individuals who are biologically male but live and dress as women. They are known as Omeggid—a third gender deeply respected and integrated into Guna society and mythology.\r\nThe \"Craters\": While tourists wonder why the road isn't fixed, locals understand the complex tension between the Panamanian government’s investment and the Guna’s desire to limit mass encroachment on their primary forest. The \"difficult road\" acts as a natural filter, keeping the islands from becoming a high-traffic resort zone.\r\n",
                        HiddenCost =
                            "Guna Yala Entrance Fee: $20 USD for foreign visitors (cash only at the checkpoint).\r\nPort Access Fee: $2 per person.\r\nVehicle Fee: $3 per car to enter the port area.\r\nParking Fee: Approximately $3 per day if you are leaving your car for an overnight stay.\r\nIsland Landing Fees: Expect to pay $3 per person at almost every island your boat visits.\r\nBoat Shuttles: Generally range from $30 to $50 per person if not pre-booked as part of a package.\r\n",
                        NearbyComplements =
                        [
                            "El Llano-Cartí Road: A destination in itself for those who enjoy rugged, scenic mountain driving.",
                            "Texaco Station: The essential \"staging area\" for every traveler heading to Guna Yala.",
                            "The Checkpoint: A great spot to stretch your legs and prepare your documents",
                        ],
                        BestTimeToVisit =
                            "Early Morning: Aim to reach the port by 8:30 AM to catch the first wave of boat departures.\r\nDry Season (Dec–April): This is when the mountain road is most stable and less prone to mud.\r\n",
                        crowdLevel =
                            "High (at the Port): Between 8:30 AM and 10:30 AM, the port is a busy hub of day-trippers and supply boats. → 8/10\r\nOnce you leave the dock, the crowds disappear into the 365 islands.\r\n",
                        Accessibility =
                            "Rating: 2/10: Extremely difficult due to the deteriorated mountain road and the requirement for a 4x4. Not wheelchair accessible or suitable for those with severe back issues or medical sensitivities.",
                        IdealDuration =
                            "30–60 Minutes: This is a transit point. You want to move through the port logistics as quickly as possible to get to the water.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 30,
                    MinCost = 50,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = gunaCityIds,
                    LanguageIds = cartiLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = cartiCatIds,
                    TagIds = cartiTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Isla Perro",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/isla-perro.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "You don’t really notice when things start to slow down.\r\nIt happens somewhere between the boat ride and the moment your feet touch the sand.\r\nIsla Perro is small, almost surprisingly so. Just a stretch of white sand, a few palm trees, and water so clear it doesn’t feel real at first. There are no roads or big structures, just a few simple huts run by the Guna people who have lived here long before anyone thought of it as a destination.\r\nMost people come here for the water, but they stay for something harder to explain. Just a short swim from the shore, there’s a shipwreck resting beneath the surface. Over time, it has blended into the reef, becoming part of the ocean rather than something separate from it. Fish move through it like it has always been there, and when you’re floating above it, everything feels quiet in a way that’s difficult to find anywhere else.\r\nBack on the island, life is simple. Fresh fish, coconut rice, and conversations that don’t feel rushed. The Guna culture is present in small but meaningful ways, from handmade molas to the rhythm of daily life that doesn’t try to impress you, only to exist as it always has.\r\nNot far from here, Isla Diablo offers something more social and energetic. But Isla Perro feels different. It doesn’t ask for your attention. It doesn’t try to stand out.\r\nAnd maybe that’s why it stays with you.\r\nBecause nothing here is trying to be more than it is.\r\n",
                        Directions =
                            "How to get there: There are no public buses or taxis to the islands. You must book a 4x4 transport service from Panama City that takes you to the port of Cartí.\r\nThe Route: The drive is approximately 2.5 to 3 hours through the mountains. From the port, you take a 20–30 minute boat ride (lancha) through the Lemon Cays to reach the island.\r\nThe Dock: You will arrive at a small, bustling beach. Isla Perro (Dog Island) is famous for the sunken Army freighter located just a few yards from the shore, making it the most iconic snorkeling spot in San Blas.\r\n",
                        WhatToKnow =
                            "Bring cash only: There are no ATMs or card machines.\r\nThe Shipwreck: This is the main attraction. You can swim from the beach directly to the wreck, which is teeming with tropical fish and coral.\r\nThe Vibe: It is a \"bucket list\" destination. Because it is so beautiful and close to the port, it is very popular with day-trippers.\r\nFacilities: Very basic. There are rustic bathrooms and a small kitchen serving fresh catch-of-the-day.\r\nSnorkel Gear: While some tours provide it, it’s best to bring your own mask and fins for a better fit.\r\nLimited Electricity: Don't expect to charge your devices; bring a power bank.\r\nEssentials: Bring a towel, a change of clothes, and a dry bag for the boat ride.\r\nBring water & snacks: Essential for staying hydrated between snorkeling sessions.\r\nBring a towel: For drying off after swimming to the nearby sandbars.\r\nBring motion sickness tablets: The boat ride from the mainland can be rough depending on the wind.\r\n",
                        ThingsToBeWaryOf =
                            "Sun Exposure: The lack of shade and the intensity of the tropical sun can lead to severe sunburn.\r\nDehydration: Saltwater and sun make it easy to forget to drink enough water\r\n.Sea Conditions: While usually calm, the 30-minute lancha ride can be bumpy.\r\nBasic Amenities: Bathrooms are very rustic and there is no Wi-Fi or electricity.\r\nWildlife: Occasional jellyfish sightings near the reef.\r\n",
                        LocalPerspective =
                            "To the Guna families who protect it, Isla Perro is more than just a beach; it is the gateway to the underwater history of the Guna Yala. While travelers see a \"sunken ship,\" locals see a thriving artificial reef that they have preserved by restricting large-scale development. The perspective here is one of quiet stewardship, offering a simple traditional lunch of fish and rice  to guests before the island returns to its natural silence once the day-trip boats depart.",
                        HiddenCost =
                            "Guna Yala Entrance Tax: $20 per person for international visitors, collected at the border.\r\nPort and Docking Fees: Small fees (approx. $2) at the embarkation point.\r\nEquipment Rental: If you don't bring your own snorkel gear, expect a rental fee.\r\nLobster Surcharge: If you choose lobster over the standard fish lunch, there is an extra charge.\r\nTransport: The 4x4 vehicle from Panama City to the coast is a significant separate cost.\r\n",
                        NearbyComplements =
                        [
                            "The Pool: A famous shallow sandbar nearby with crystal clear, waist-deep water.",
                            "Isla Diablo: Its neighbor island, perfect for those looking for a more social \"beach club\" atmosphere.",
                            "Island Hopping: Most tours include stops at 2–4 different islands in one day.",
                        ],
                        BestTimeToVisit =
                            "Dry Season (Dec–April): Best for clear skies and optimal snorkeling visibility.\r\nJan–March: Peak water clarity for seeing the shipwreck.\r\nWeekdays: To fully appreciate the \"quiet\" reputation you mentioned, avoiding the weekend crowds from the city.\r\nMorning: The best time for snorkeling before the afternoon winds pick up.\r\n",
                        crowdLevel =
                            "Medium 6/10: While quieter than Diablo, its popularity for snorkeling means it sees steady visitors during the day. It is significantly more visited than remote spots like the Dutch Cays , but far less crowded than typical resort beaches in Bali or Cancun.",
                        Accessibility =
                            "Rating: 4/10\r\nLong car ride\r\nBumpy mountain road\r\nBoat transfer required\r\nNot wheelchair accessible\r\nNot suitable for people with severe motion sickness\r\nNot suitable for people needing medical facilities nearby\r\n",
                        IdealDuration =
                            "4–6 hours: A standard day trip provides ample time for snorkeling and lunch.\r\nOvernight: Staying 1 night allows you to experience the island’s true quiet once the day-trip boats leave.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 250,
                    MinCost = 80,
                    SafetyLevel = 8,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = gunaCityIds,
                    LanguageIds = gunaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = perroCatIds,
                    TagIds = perroTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Ibin’s Beach Restaurant (Isla Banedup)",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/ibins-restaurant.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Have you ever stumbled upon a place so untouched it feels like the world paused just for you?\r\nIbin’s Beach Restaurant is that place. Tucked away on one of San Blas’ most remote islands, it’s a secret the Caribbean has been quietly keeping. You can’t drive here. You can’t book it from a busy street corner. The only way in is on a 5-night or longer catamaran charter, gliding across turquoise waters, guided away from the crowds, toward silence and sun-soaked beauty.\r\nThe moment you step onto Ibin’s wooden pier, the air shifts. The smell of saltwater mixes with the sizzle of freshly caught seafood. Rustic wooden tables, handcrafted décor of shells and corals, and flags fluttering in the Caribbean breeze create an atmosphere that’s warm, simple, and alive. You don’t just see the ocean, you feel it. The water stretches endlessly, shimmering under the sun, inviting you to slow down.\r\nEvery meal here tells a story. Lobster pulled straight from the sea, octopus curry that surprises your taste buds, cocktails crafted with local rum, all made with care and a deep respect for flavor. But it’s not just the food. It’s the conversations around the table, the laughter, the feeling that for these few hours, you belong to this island as much as the waves and the wind do.\r\nIn a world of crowded beaches and rushed experiences, Ibin’s stands apart. It’s not about luxury, it’s about connection. Connection to nature, to flavor, and to moments that linger long after you leave. Here, time slows. The world outside fades. And for a little while, you can just be.\r\nIbin’s Beach Restaurant isn’t just a stop on a map. It’s a place where memories are made, where stories begin, and where the quiet magic of San Blas comes alive.\r\n",
                        Directions =
                            "How to get there: There are no taxis here. Ibin’s is accessible exclusively by boat, specifically via catamaran charters.\r\nThe Route: Most visitors reach it through 5-night or longer charters that navigate the outer cays of San Blas (the Dutch Cays).\r\nThe Dock: You’ll arrive at a rustic wooden pier on Banedup Island. Look for the sign about \"friends arriving with the wind.\"\r\n",
                        WhatToKnow =
                            "Bring cash only\r\nThe Menu: It’s dictated by the ocean. Expect the freshest lobster, tuna, and octopus curry.\r\nThe Vibe: It’s \"pirate-chic\"—think wooden planks, flags, and tables literally sitting in the water if you want to cool your feet while you eat.\r\nService: Ibin is known for delivering fresh, warm coconut rolls or focaccia directly to anchored boats in the morning.\r\nLimited electricity\r\nSometimes no Wi-Fi\r\nBring sunscreen\r\nBring water\r\nBring snacks\r\nBring towel\r\nBring motion sickness tablets (boat can be rough)\r\n",
                        ThingsToBeWaryOf =
                            "Rough boat rides\r\nStrong sun (very strong)\r\nDehydration\r\nJellyfish sometimes\r\nNo medical facilities\r\nWeather can cancel boats\r\nVery basic bathrooms on some islands\r\nSand flies / mosquitoes in evening\r\n",
                        LocalPerspective =
                            "Ibin is the soul of this place. He isn’t just a \"business owner\"; he’s a man who returned home to San Blas after years of cooking for celebrities and politicians in Panama City. He treats guests like family because, on an island this small, everyone is. You’ll see the Guna culture reflected not in a museum, but in the way the food is caught and the way the conversation flows.",
                        HiddenCost =
                            "Guna Territory Entrance Fees: While the restaurant is part of the experience, the Guna Yala region has independent entry and port taxes (usually $20+ per person) that are almost never included in charter prices\r\nPort fee\r\nBoat transfer fee\r\nSnorkel rental\r\nDrinks\r\nLobster extra charge\r\nOvernight hut fee\r\nTransport from Panama City\r\n",
                        NearbyComplements =
                        [
                            "The Pool: A famous, shallow sandbar area nearby with crystal clear water, perfect for wading.",
                            "Snorkeling: The Dutch Cays offer some of the most vibrant marine life in the Caribbean because the reefs are so far from the mainland.",
                            "Starfish island",
                        ],
                        BestTimeToVisit =
                            "Mid December – April (dry season) →This is when the \"trade winds\" are up, making the 5-day sail to reach Ibin’s much more pleasant.\r\nJanuary – March → best water clarity\r\nWeekdays → less crowded\r\nMorning → best water color\r\nMidday → very hot\r\nLate afternoon → boats start returning\r\n",
                        crowdLevel =
                            "Very low 3/10: Because it’s so deep in the archipelago, you’ll only share the space with a handful of other sailors.",
                        Accessibility =
                            "Ratings: 4/10\r\nLong car ride\r\nBumpy mountain road\r\nBoat transfer required\r\nNot wheelchair accessible\r\nNot suitable for people with severe motion sickness\r\nNot suitable for people needing medical facilities nearby\r\n",
                        IdealDuration = "2–4 hours or half a day.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 50,
                    MinCost = 30,
                    SafetyLevel = 8,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = gunaCityIds,
                    LanguageIds = gunaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = ibinCatIds,
                    TagIds = ibinTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Isla Diablo",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/isla-diablo.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "You usually hear about San Blas as a place to slow down.\r\nQuiet islands. Empty beaches."
                            + " Not much going on.\r\nIsla Diablo feels a little different from that.\r\nIt’s still beautiful in the"
                            + " way all the islands are,  clear water, soft sand, and small stretches of land surrounded by nothing"
                            + " but ocean. But when you arrive, you notice something else. There are more people. Boats come in more"
                            + " often. There’s movement, conversation, a bit of energy that you don’t always find on the quieter islands"
                            + " nearby.\r\nIt’s not overwhelming, just noticeable.\r\nPeople come here in groups, and it shows. Friends"
                            + " sitting together under the palms, others heading in and out of the water, some just staying by the shore"
                            + " and talking. It’s easy to find yourself in a conversation without planning to.\r\nNot too far from here,"
                            + " Isla Perro Chico gives you a different kind of moment. There’s a shipwreck just off the island, and most"
                            + " people end up in the water at some point, swimming out to see it. It’s simple, but it stays with you.\r\nBack"
                            + " on Isla Diablo, things stay straightforward. Fresh food, a small beach bar, and the kind of setup you see"
                            + " across San Blas. The island is run by the Guna people, and like most places here, it feels less like something"
                            + " built for visitors and more like something you’re being allowed into for a while.\r\nIt’s not the quietest"
                            + " island, and it doesn’t try to be.\r\nBut if you’re looking for a place where the experience includes other"
                            + " people as much as the setting itself, this is usually where you end up remembering a few faces, not just"
                            + " the view.\r\nr",
                        Directions =
                            "How to get there: There are no taxis in the San Blas region; Isla Diablo is accessible exclusively by boat. Most visitors reach the island by arranging a private lancha (small motorboat).\r\nThe Route: The island is located within the Lemon Cays, approximately 30 minutes by boat from the mainland port. The journey typically begins with a 2.5-hour drive from Panama City to the entry point at Cartí.\r\nThe Dock: You will arrive at a lively beach managed in rotation by local Guna families, featuring simple wooden cabins and hammocks tied to palm trees over the water. The island is positioned a short distance from the iconic sunken shipwreck at Isla Perro Chico.\r\n",
                        WhatToKnow =
                            "Bring cash only: There are no card readers in the middle of the ocean.\r\nThe Vibe: Energetic and social. It’s the \"backpacker hub\" of San Blas—think young travelers, music, and shared stories.\r\nManagement: The island is managed in rotation by different Guna families, giving it a true Caribbean community feel.\r\nThe Food: Simple and authentic. Expect fresh fish, coconut rice, and local fruit.\r\nSnack Bar: There is a convenient spot on the beach for quick bites and drinks.\r\nAccommodation: Very basic, traditional Guna cabins. It’s about the location, not the thread count.\r\nBring sunscreen: The white sand reflects the sun like a mirror.\r\nBring water & snacks: While there is a kitchen, having your own supplies is essential.\r\nBring a towel: You’ll be in and out of the water all day.\r\nBring motion sickness tablets: The boat ride from the port can be bouncy.\r\n",
                        ThingsToBeWaryOf =
                            "Not for \"Peace and Quiet\": If you want total silence, this isn't your island.\r\nRough boat rides: The 30-minute crossing can get wet and bumpy.\r\nStrong sun: It’s easy to get burned before you even finish your first drink.\r\nBasic Facilities: Bathrooms and showers are functional but very simple.\r\nSand flies: They can be active in the evening; bring repellent.\r\nNo medical facilities: You are remote; act with caution.\r\n",
                        LocalPerspective =
                            "The Guna Heartbeat: Isla Diablo (Niadub) is part of an autonomous indigenous reservation. The Guna people aren't just \"staff\" at a resort; they are the owners and protectors of the land.\r\nThe \"Fee\" Culture: You will encounter fees for entry, for docking, and for island hopping. This is the local way of maintaining their sovereignty and ensuring the tourism industry directly benefits the community rather than outside corporations.\r\nA Protected Paradise: Locals take immense pride in the fact that San Blas remains \"untouched.\" You won't find traditional \"modern\" luxuries because the community prioritizes the preservation of their ancestral culture over commercial development.\r\nMaster Roshi’s Reality: To locals, the tiny, palm-fringed islands aren't a cartoon or a movie set—they are a delicate ecosystem. They expect visitors to respect the \"leave no trace\" philosophy, as the resources on these small patches of sand are incredibly limited.\r\n",
                        HiddenCost =
                            "Guna Territory Entrance Fees: Usually $20+ per person, paid at the border.\r\nPort fee: A small fee (approx. $2) at the embarkation point.\r\nBoat transfer fee: Usually not included in the \"island stay\" price.\r\nIsland Hopping: Small fees if you want to visit neighboring islands.\r\nDrinks & Lobster: If you want to upgrade from the basic meal, bring extra cash.\r\nTransport from Panama City: The 4x4 ride to the coast is a separate cost.\r\n",
                        NearbyComplements =
                        [
                            "Isla Perro: Just a few minutes away, home to the famous sunken shipwreck.",
                            "Achutupu: A private island nearby with a thriving Guna population and authentic handicraft markets.",
                            "The Lemon Cays: A beautiful group of surrounding islands with distinct personalities.",
                            "Snorkeling: The reefs around the shipwreck are some of the most accessible in the area.",
                        ],
                        BestTimeToVisit =
                            "Mid-December – April: The dry season offers the best weather and clearest water.\r\nWeekdays: If you want the \"lively\" vibe without the \"crowded\" vibe.\r\nMorning: Best for boat crossings before the winds pick up in the afternoon.\r\n",
                        crowdLevel =
                            "High: For San Blas standards, this is a popular spot. → 8/10\r\nIt’s the place to meet people, but you can always find a corner of the beach to yourself.\r\n",
                        Accessibility =
                            "Ratings: 4/10\r\nAccessibility is moderate to difficult:\r\n2.5-hour 4x4 ride through winding mountains.\r\n30-minute boat ride.\r\nNot wheelchair accessible.\r\nNot suitable for those with severe back issues or medical needs.\r\n",
                        IdealDuration =
                            "Day trip: 4–6 hours to snorkel and eat.\r\nOvernight: 1–2 nights is the sweet spot to soak up the evening energy.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 135,
                    MinCost = 250,
                    SafetyLevel = 7,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = gunaCityIds,
                    LanguageIds = gunaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = diabloCatIds,
                    TagIds = diabloTagIds,
                },
                new DestinationDto
                {
                    DestinationName = "Cayos Holandeses",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/dutch-cays.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Most people stop at the first few islands they see, satisfied with the turquoise"
                            + " water and the shade of a palm tree. But there is a specific kind of magic reserved for those"
                            + " who keep going—past the day-trippers and the busy ports—until they reach the Cayos Holandeses."
                            + "\r\nIt is the \"edge\" of the San Blas archipelago. Here, the silence is heavier, the sand is"
                            + " whiter, and the water is so clear it feels like the boats are hovering in mid-air. It is a place"
                            + " that doesn't just offer an escape; it demands a total surrender to the elements.\r\nWe spend our"
                            + " lives trying to be \"connected,\" but when you reach the Dutch Cays, you realize that the most"
                            + " important connections happen when the signal bars disappear. It’s a reminder that the world is"
                            + " still capable of being untouched, and that sometimes, the best things in life are found at the"
                            + " very end of the map.",
                        Directions =
                            "How to get there: You cannot reach the Dutch Cays by a standard day-trip lancha. You"
                            + " must book an \"expedition\" or an overnight stay. It requires a 4x4 transport from Panama City to"
                            + " the port of Cartí.\r\nThe Route: Unlike the 30-minute hop to Isla Perro, the boat ride to the"
                            + " Dutch Cays takes 1 to 1.5 hours across open water. It is a long, often bumpy journey that crosses"
                            + " the \"outer wall\" of the inner islands.\r\nThe Dock: You won't find a centralized pier. You arrive"
                            + " directly onto the sand of islands like Wegodub or Diadub. These are the northernmost cays, protected"
                            + " by a massive natural reef barrier.\r\n",
                        WhatToKnow =
                            "The Menu: Entirely dictated by the sea. Expect fresh lobster, snapper, and octopus curry."
                            + "\r\nDigital Detox: Expect very limited to zero cell service and no Wi-Fi. It is an ideal place to"
                            + " disconnect.\r\nService: In some areas, you may receive fresh coconut rolls or focaccia delivered"
                            + " directly to your boat in the morning.\r\nThe Vibe: \"Pirate-chic\"—rustic wooden planks, handcrafted"
                            + " shell décor, and total immersion in nature.\r\nEssentials: Bring reef-safe sunscreen, a towel, and a"
                            + " dry bag for gear.\r\nMotion Sickness: Because these are the outer cays, the boat ride can be rough;"
                            + " bring tablets if you are sensitive.\r\n",
                        ThingsToBeWaryOf =
                            "Isolation: There are no medical facilities nearby; you are truly off-grid.\r\nStrong Sun:"
                            + " The UV exposure here is extreme due to the white sand and clear water.\r\nWeather Disruptions: Strong winds"
                            + " or rough seas can occasionally cancel boat transfers or make the trip much longer.\r\nBasic Facilities: Expect"
                            + " very simple, rustic huts and shared bathrooms.\r\nBugs: Sand flies and mosquitoes can be active in the evenings;"
                            + " bring repellent.\r\n",
                        LocalPerspective =
                            "The \"Brisa\" Check: A random tourist sees a sunny day and wants to go to the cays."
                            + " A local looks at the \"Brisa\" (the North Trade Winds). If your Guna captain says the water is too "
                            + "\"bravo\" (wild), respect it. The crossing to the Dutch Cays involves open-sea swells that only a"
                            + " local knows how to navigate safely.\r\nThe Matriarchal Guard: These islands are the pride of the"
                            + " Guna women. In their society, women are the property owners and the protectors of the land. When"
                            + " you see a Guna woman in her traditional mola, she isn't just wearing \"folk art\"—she is wearing the"
                            + " history of her lineage, and her presence on these remote cays is a sign that the territory is being"
                            + " actively guarded.\r\nThe \"Ulu\" Tradition: While tourists want kayaks, locals use the Ulu (a dugout canoe)."
                            + " If you see a Guna fisherman miles from the beach in a tiny wooden boat, he is likely diving 30+ feet"
                            + " on a single breath to catch the lobster that will be on your plate.\r\nThe \"Third Reef\": Tourists"
                            + " snorkeling near the beach see fish; locals know the way to the \"Third Reef\"—the outermost coral wall"
                            + " where nurse sharks, eagle rays, and massive sea fans live. You only go here with a guide who knows the"
                            + " specific \"breaks\" in the reef.\r\n",
                        HiddenCost =
                            "Guna Territory Entrance Fees: Approximately $20+ per person, usually not included in charter or "
                            + "tour prices.\r\nPort and Transfer Fees: Small fees at the dock and for the lancha transfer to the outer cays."
                            + "\r\nThe \"Real\" Total: While a tour might be advertised at a base price, after entrance fees, drinks, and"
                            + " transport from Panama City, the final cost often increases by $30–$50.\r\nUpgrades: Fresh lobster often"
                            + " carries an extra charge over the standard fish meal.\r\n",
                        NearbyComplements =
                        [
                            "The Pool: A famous, shallow sandbar area with waist-deep water in the middle of the ocean.",
                            "Snorkeling: The Dutch Cays offer the most vibrant marine life in San Blas because the reefs are far from mainland runoff.",
                            "Island Hopping: Explore various tiny, uninhabited islets like the Starfish Islands.",
                            "Kayaking & Paddleboarding: The calm, protected lagoons are perfect for exploring the reefs from above.",
                        ],
                        BestTimeToVisit =
                            "Mid-December – April (Dry Season): The \"trade winds\" are up, making sailing trips more"
                            + " pleasant.\r\nJanuary – March: This period typically offers the best water clarity for snorkeling.\r\nMorning:"
                            + " The best time to see the water at its most vibrant turquoise color.\r\n",
                        crowdLevel =
                            "Very Low (3/10): Because of the distance and the requirement for overnight stays, you will only share"
                            + " these islands with a handful of other travelers.",
                        Accessibility =
                            "Rating: 4/10: This is a difficult journey involving a 2.5-hour mountain drive followed by a long,"
                            + " potentially bumpy boat ride. It is not wheelchair accessible or suitable for those needing nearby medical facilities.",
                        IdealDuration =
                            "2–3 Nights: The minimum time needed to justify the long journey to the outer cays.\r\n5+ Days:"
                            + " For those on sailing charters who want to truly immerse themselves in the \"slow life\" of the archipelago.\r\n",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.OctToDec,
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 385,
                    MinCost = 200,
                    SafetyLevel = 8,
                    TimeZone = "Eastern Standard Time",
                    CountryId = panamaCountryId,
                    CityIds = gunaCityIds,
                    LanguageIds = gunaLangIds,
                    CurrencyIds = panamaCurrencyId,
                    CategoryIds = holanCatIds,
                    TagIds = holanTagIds,
                },
            ];

            List<DestinationDto> tokyoDestinationList =
            [
                // 1. Meiji Shrine
                new DestinationDto
                {
                    DestinationName = "Meiji Shrine",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/meiji.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Have you ever noticed how the air changes when you step off a busy street and into a forest?\n\nIn Tokyo, that transition happens in a single step. One moment you are in Harajuku, surrounded by neon lights and the frantic energy of the city. The next, you are walking under a massive wooden gate, and the sound of traffic is replaced by the crunch of gravel under your boots.\n\nThis is Meiji Jingu. It isn't just a shrine; it's a 170-acre heartbeat of silence in the middle of the world's most populated city.\n\nWhat to Do:\n\nWalking Tour: Follow a self-guided route through the 40-acre historic district to see the Metropolitan Cathedral, which took over 100 years to build.\n\nMuseums: Visit the Panama Canal Museum or the Museo de la Mola to understand the engineering and cultural foundations of the country.\n\nGovernmental Sights: Walk past the Palacio de las Garzas, the official residence and office of the President.\n\nSunset Rituals: Head to one of the many rooftop bars to watch the sunset paint the modern city skyline gold while you sit in the historic heart.\n\nCoffee Culture: Sample Panama's world-famous Geisha coffee at locally-owned specialty shops.",
                        Directions =
                            "Best Station: Harajuku Station (JR Yamanote Line) or Meiji-jingumae Station (Chiyoda & Fukutoku Lines).\n\nKey Exit: From JR Harajuku Station: Take the Omotesando Exit. From Meiji-jingumae Station: Take Exit 2.\n\nThe Direction Walk: Once you step out of the station, look for the large stone bridge (Jingu-bashi). Cross the bridge and you will immediately see a massive wooden Torii gate nestled in the forest. Enter through the gate; the main shrine buildings are about a 10-minute walk along the shaded forest path.\n\nAddress for Taxi: 東京都渋谷区代々木神園町1-1",
                        WhatToKnow =
                            "Currency Conversion: (Based on a rounded rate of $1 = 150¥) – Main Grounds: Free. Inner Garden: 500¥ (~$3.33 USD). Meiji Jingu Museum: 1,000¥ (~$6.67 USD).\n\nShinto Rituals: You can write your wishes on an ema (wooden tablet) or purchase an omamori (amulet) for protection. There is English signage to guide you through the 2-bow, 2-clap, 1-bow prayer ritual.\n\nPhotographic Etiquette: Photography is generally permitted on the grounds but is strictly forbidden inside the main sanctuary buildings and certain museum exhibits.\n\nPlanning Tip: Make sure to carry physical cash (specifically 1,000 yen notes and coins). While the Museum and some larger shops may take cards, the garden entrance and ritual items (charms, tablets, fortunes) are strictly cash-only.\n\nPro-Tip: If you see a wedding procession, it is polite to stand to the side and observe quietly. It is a frequent and beautiful occurrence at Meiji Jingu, especially on weekend mornings.",
                        ThingsToBeWaryOf =
                            "Gravel Paths: The primary paths are made of thick gravel. While beautiful, they can be tiring for those in thin-soled shoes and difficult for strollers or wheelchairs with small wheels.\n\nThe New Year Rush: Between January 1st and 3rd, the shrine sees over 3 million visitors. Avoid these dates unless you specifically want to experience the intense \"hatsumode\" (first prayer) crowds.\n\nSunrise/Sunset Hours: The shrine does not have fixed clock hours; it opens at sunrise and closes at sunset. Check the local sun schedule if you are visiting in the late afternoon.\n\nEtiquette: Avoid standing directly in the center of the Torii gates or the center of the path, as this space is reserved for the deities.",
                        LocalPerspective =
                            "The \"Power Spot\": Visit Kiyomasa's Well in the Inner Garden. Locals consider it a source of positive, restorative energy. It is especially popular in the middle of June when the irises are in bloom.\n\nWedding Watching: If you visit on a weekend morning, keep your eyes peeled for a traditional Shinto wedding procession. You will often see the bride in a white kimono (shiromuku) led by priests under a large red umbrella.\n\nEnglish Ease: The shrine is very tourist-friendly with extensive English signage explaining rituals, such as how to purify your hands at the temizuya or make an offering.",
                        HiddenCost =
                            "The Inner Garden Maintenance Fee: 500¥ (~$3.33 USD). To access the famous iris gardens and Kiyomasa's Well (the \"power spot\"), you must pay a maintenance fee at the garden entrance.\n\nMeiji Jingu Museum: 1,000¥ (~$6.67 USD). This modern facility houses treasures and personal effects of the Emperor and Empress. High school students and younger are 900¥ (~$6.00 USD).\n\nSaisen (Offertory Coins): 5¥ to 100¥+ (~$0.03 – $0.67 USD). It is customary to toss a coin into the wooden offering box (saisen-bako) before praying. The 5-yen coin is preferred as \"go-en\" (5 yen) is a homophone for \"honorable good luck\" or \"connection\".\n\nEma (Wooden Prayer Tablets): ~500¥ to 1,000¥ (~$3.33 – $6.67 USD). If you wish to write a prayer or goal to hang at the shrine, you must purchase a wooden tablet at the amulet stall.\n\nOmamori (Amulets & Charms): ~500¥ to 1,500¥ (~$3.33 – $10.00 USD). These colorful silk pouches for protection, health, or academic success are popular purchases but can be expensive if buying several.\n\nOmikuji (Fortunes): ~100¥ to 200¥ (~$0.67 – $1.33 USD). Unlike many shrines, Meiji Jingu's fortunes are unique \"Waka\" poems written by the Emperor and Empress rather than \"good/bad luck\" predictions.\n\nGoshuin (Temple Stamps): ~300¥ to 500¥ (~$2.00 – $3.33 USD). If you collect temple stamps in a Goshuin-cho book, there is a fee for the priest to hand-calligraph the shrine's seal for you.\n\nCoin-Operated Lockers: 300¥ to 700¥ (~$2.00 – $4.67 USD). If you are coming directly from the airport or have heavy bags, you will likely need to use lockers at Harajuku or Yoyogi stations, as large luggage is difficult to navigate on the thick gravel paths.\n\nThe \"Gravel Toll\": While not a direct fee, the thick gravel paths can be hard on shoes. Many visitors find themselves buying more supportive footwear or paying for a taxi later in the day due to foot fatigue.",
                        NearbyComplements = new List<string>
                        {
                            "Yoyogi Park: Located directly adjacent, this park hosts weekend food fairs, flea markets, and rockabilly dance performances.",
                            "Harajuku (Takeshita Street): The epicenter of teen fashion and \"Kawaii\" culture is just steps from the southern torii gate.",
                            "Omotesando: Known as Tokyo's \"Champs-Élysées,\" this nearby boulevard offers high-end shopping and stunning modern architecture.",
                            "Shibuya Crossing: A 15-minute walk or one train stop away, providing a sharp contrast between the shrine's tranquility and the world's busiest intersection.",
                        },
                        BestTimeToVisit =
                            "Spring Grand Festival (Late April – Early May): The best time to witness traditional ceremonial music and dances.\n\nMid-June: Specifically for the Inner Garden, as this is when the expansive iris beds are in full bloom.\n\nAutumn: The forest provides a cool, shaded canopy during Tokyo's humid months, and the colors of the surrounding Yoyogi Park are beautiful in late November.\n\nDaily Timing: Arrive at sunrise (typically around 5:00 AM–6:00 AM) to experience the forest in near-total silence before the tour groups arrive at 10:00 AM.",
                        crowdLevel =
                            "High (8/10): As one of Tokyo's top landmarks, it is consistently busy. Avoid: January 1st–3rd, when over 3 million people visit for New Year's prayers (Hatsumode), making the paths nearly impassable. Weekends: Expect a high volume of local families and traditional wedding processions.",
                        Accessibility =
                            "Rating: 8/10\n\nThe Terrain: The paths are wide and flat but covered in heavy gravel. This can be physically taxing for long walks and difficult for strollers or wheelchairs with thin tires.\n\nFacilities: Modern, accessible restrooms are located near the main sanctuary and the museum.\n\nTransit: Both Harajuku and Meiji-jingumae stations have elevator access, making the journey to the shrine gates very accessible.",
                        IdealDuration =
                            "1.5 to 2.5 Hours\n\nThe walk from the entrance to the main complex takes about 10–15 minutes each way. Allow an extra 45 minutes if you plan to explore the Inner Garden and Kiyomasa's Well.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 4,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = meijiCatIds,
                    TagIds = meijiTagIds,
                },
                // 2. Takeshita Street
                new DestinationDto
                {
                    DestinationName = "Takeshita Street",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/takeshita.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Takeshita Street is the 350-meter-long, high-energy artery of Harajuku and the undisputed global epicenter of \"Kawaii\" (cute) culture. Located directly across from the historic Harajuku Station, this pedestrian paradise is a sensory explosion of neon signs, J-pop soundtracks, and trend-setting youth fashion. From gothic lolita and \"yume-kawaii\" pastels to independent designer boutiques and massive cotton candy, the street is a living runway where individuality is the only requirement. Even if you aren't shopping for a new wardrobe, the street serves as a vibrant theater of modern Japanese subcultures.\n\nWhat to Do:\n\nThe Crepe Ritual: Visit Marion Crepes or Santa Monica Crepes. In Harajuku, holding a paper-wrapped crepe filled with whipped cream and fruit is the unofficial entry requirement.\n\nPurikura Memories: Head to a photo booth basement. These aren't normal booths; they offer filters that enlarge your eyes, smooth your skin, and let you add digital \"kawaii\" stickers to your prints.\n\nFashion Hunting: Explore shops like WEGO or independent designer boutiques. You'll find everything from vintage \"Ura-Harajuku\" streetwear to \"Yume-Kawaii\" (dreamy cute) pastel aesthetics.\n\nInstagrammable Snacking: Look for the massive, multi-colored rainbow cotton candy at Totti Candy Factory or the super-long soft-serve ice cream that defies gravity.\n\nOtaku Exploration: Explore the many shops selling anime merchandising, limited-edition figures, and streetwear that draws \"otaku\" and anime fans from across the globe.\n\nPeople Watching: Find a spot near the entrance to simply observe the \"Harajuku girls\" and fashionistas who treat the sidewalk like a high-fashion runway.",
                        Directions =
                            "Best Station: JR Harajuku Station (Yamanote Line) or Meiji-jingumae Station (Chiyoda & Fukutoshin Lines).\n\nKey Exit: Exit through the Takeshita Exit at JR Harajuku Station.\n\nThe Direction Walk: Once you exit the station, cross the street at the designated crosswalk. You will see the iconic \"Takeshita-dori\" arched gate directly in front of you.\n\nAddress for Taxi: 東京都渋谷区神宮前1丁目 (Jingumae 1-chome, Shibuya City, Tokyo).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Most snacks (crepes, cotton candy) cost between 600¥ and 1,000¥ (~$4.00 – $6.70 USD).\n\nCash Still Matters: While many fashion stores take cards, the smaller food stalls and older purikura machines are strictly cash-only.\n\nNo Trash Cans: Japan has very few public trash cans. Most people eat their snacks in front of the shop where they bought them, as walking and eating is generally discouraged, and the shop will have a bin for your wrapper.\n\nOperating Hours: Most shops open around 10:30 AM or 11:00 AM and close by 8:00 PM. It is not a late-night destination.\n\nVehicle Restrictions: The street is a pedestrian-only zone daily from 11:00 AM to 6:00 PM, meaning you can walk freely without worrying about traffic.\n\nNo Public Amenities: Be aware that there are no public coin lockers or free Wi-Fi directly on the street itself. Most shops do not provide information boards in foreign languages.\n\nFood Etiquette: While \"snacking as you go\" is the draw here, try to stand near the shop where you purchased your food or dispose of your trash in their specific bins, as public trash cans are rare in Tokyo.",
                        ThingsToBeWaryOf =
                            "The Human Wave: On weekends and holidays, the crowd level is a 10/10. It can be overwhelming if you have claustrophobia.\n\nAggressive Promoters: You may see \"scouts\" or promoters (often for clothing stores or hair salons) near the entrance. A polite but firm \"No, thank you\" or a head shake while walking is enough.\n\nAnimal Cafes: Harajuku has many owl, cat, and otter cafes. Be mindful of the ethical standards and the well-being of the animals before choosing to visit.\n\nPhotography Limits: While the street is a photo-op, some boutiques and purikura areas strictly forbid indoor photography of their merchandise or layouts.",
                        LocalPerspective =
                            "Beyond the Main Drag: Locals know the \"real\" Harajuku is in the side alleys. If the main street feels too crowded, duck into the small veins branching off; that's where the best hidden vintage shops and quiet coffee spots are.\n\nA Cultural Identity: For Japanese youth, this isn't a \"tourist trap.\" It's a space where they can express themselves freely away from the rigid expectations of school or corporate life.\n\nLeft-Hand Rule: To keep the flow of thousands of people moving, locals generally stick to the left side of the street. Follow suit to avoid \"pedestrian gridlock.\"\n\nFashion Watch: If you want to see the most creative outfits, the street serves as a gathering point for people dressed as their favorite anime or manga characters, especially on weekend afternoons.",
                        HiddenCost =
                            "The \"Instagram Tax\": Those rainbow snacks and themed cafes look amazing, but they are priced for the \"aesthetic.\" You are often paying $8 for sugar and a photo opportunity.\n\nGachapon Addiction: You will see walls of capsule toy machines (Gachapon). At 300¥ to 500¥ a pop, it's easy to lose 2,000¥ before you realize it.\n\nNo Public Wi-Fi/Lockers: There is no free public Wi-Fi on the street itself. Additionally, lockers at Harajuku station fill up by 10:00 AM. If you have luggage, keep it at your hotel.\n\nPhoto Booth Add-ons: A standard Purikura session is roughly 400–500¥ (~$2.67 – $3.33 USD), but some shops charge extra for costume rentals or high-res digital downloads.\n\nThe \"Eye-Catch\" Premium: Many of the most \"Instagrammable\" foods, like the giant rainbow cotton candy, are priced significantly higher (~900¥+) than standard snacks.\n\nCrepe Customization: A basic crepe is affordable, but adding ice cream, extra fruit, or seasonal toppings can quickly push the price toward 800–1,000¥.",
                        NearbyComplements = new List<string>
                        {
                            "Meiji Jingu: Literally across the street. It is the \"silent\" counterpart to Takeshita's \"loud.\"",
                            "Cat Street: A 5-minute walk away. It's a winding pedestrian path connecting Harajuku to Shibuya, filled with high-end streetwear and hip cafes.",
                            "Daiso Harajuku: A massive multi-story 100-yen shop on the main street—perfect for budget-friendly souvenirs.",
                        },
                        BestTimeToVisit =
                            "Weekday Mornings (11:00 AM): You'll get the \"Harajuku vibe\" without the crushing weekend crowds.\n\nLate Afternoon: The lighting is best for photos, and the energy peaks as students finish school.",
                        crowdLevel =
                            "Very High (10/10): Especially on Sunday afternoons. If you want a peaceful stroll, this is not the street for you.",
                        Accessibility =
                            "Rating: 7/10\n\nThe Terrain: The street is flat and paved, which is good for wheels. However, the sheer density of the crowd makes navigating a wheelchair or stroller extremely stressful and slow.",
                        IdealDuration =
                            "1 to 2 Hours\n\nThis is enough time to walk the length, grab a snack, and browse 2-3 shops. Add more time if you plan to do a full \"Purikura\" session or visit an animal cafe.\n\nThe Terrain: The street is paved and flat, but the sheer volume of people makes navigating with a wheelchair or stroller extremely difficult and stressful during peak hours.\n\nStation Access: Harajuku Station is modern and has elevators, but the crossing into Takeshita is often congested.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 25,
                    MinCost = 5,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = takeshitaCatIds,
                    TagIds = takeshitaTagIds,
                },
                // 3. Cat Street
                new DestinationDto
                {
                    DestinationName = "Cat Street",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/cat_street.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Cat Street is the sophisticated, laid-back older sibling to the neon chaos of Takeshita Street. Officially named Kyu-Shibuya-gawa Yuhodoro (Old Shibuya River Pedestrian Lane), this half-mile stretch follows the path of a diverted underground stream. It serves as a stylish \"catwalk\" for Tokyo's fashion-forward 20 and 30-somethings, connecting the high-energy pulse of Harajuku with the urban grit of Shibuya. Unlike the \"kawaii\" overload nearby, Cat Street is defined by an understated, cool vibe—think high-end international flagships sitting comfortably next to hole-in-the-wall vintage boutiques and minimalist espresso bars.\n\nWhat to Do:\n\nVintage Treasure Hunting: Visit RAGTAG to find authenticated designer pieces from brands like Comme des Garçons and Issey Miyake at a fraction of their original cost.\n\nGourmet Snacking: Grab a world-famous lobster roll at LUKE'S LOBSTER or a handcrafted artisan treat at Good Town Doughnuts.\n\nExperiential Shopping: Explore DELSEY LAB Tokyo, a concept store designed to look like an airplane cabin, or the multi-story toy wonderland of KIDDY LAND nearby.\n\nArtisanal Coffee: Take a break at one of the many \"hipster-vibe\" cafes to people-watch the trendsetters walking the street.\n\nStreet Art Walk: Admire the stylish exteriors and murals that make the entire promenade a photogenic \"Instagrammable\" backdrop.\n\nThe Side-Alley Gamble: The \"real\" Cat Street is in the veins. Turn into any small alleyway to find hidden cafes and independent designers tucked away in residential buildings.",
                        Directions =
                            "Best Stations: Harajuku Station (JR Yamanote), Meiji-jingumae Station (Metro Chiyoda/Fukutoshin), or Shibuya Station.\n\nKey Entry Points: From Harajuku: Exit the Takeshita Exit, cross Route 305, and walk straight; Cat Street is the fifth street on your right.\n\nFrom Omotesando: It intersects the famous boulevard near the Ralph Lauren store and the pedestrian bridge.\n\nThe Direction Walk: The street is mostly pedestrianized, making it a seamless walk between the two major districts. Look for the fork in the road near the Miyashita-koen intersection in Shibuya to find the southern start.\n\nAddress for Taxi: 東京都渋谷区神宮前5-10-10 (This address is for the center-point of the street near RAGTAG).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥).\n\nThe River Legacy: The street follows a winding, organic path because it was built over the old Shibuya River bed prepared for the 1964 Olympics.\n\nAccommodation: The area features unique stays like the Trunk Hotel (Luxury) or The Millennials Shibuya (Boutique Capsule), perfect for staying in the heart of the trend.\n\nAtmosphere: It is significantly quieter and more \"grown-up\" than Takeshita Street. There is no hard-sell approach here; browsing is encouraged and relaxed.\n\nPrice Range: You can find a 500¥ doughnut or a 50,000¥ vintage jacket. It caters to everyone from students to high-earning entrepreneurs.\n\nCash for Carts: While boutiques take cards, the small food stands and artisanal coffee carts often prefer cash.",
                        ThingsToBeWaryOf =
                            "Pricing: While there are budget finds, the influx of international brands has made parts of the street quite expensive.\n\nOpening Hours: Many boutiques do not open until 11:00 AM. Arriving too early might find you in front of closed shutters.\n\nNavigation: Because it is a \"back street,\" it can be easy to miss the entrance if you aren't looking for the landmarks like the Omotesando pedestrian bridge.\n\nThe \"Takeshita\" Trap: Don't confuse the two. If you want neon and chaos, go to Takeshita. If you want a coffee and a high-end hoodie, stay on Cat Street.",
                        LocalPerspective =
                            "\"Ura-Hara\" Vibes: This area is part of \"Back-Harajuku\" (Ura-Harajuku). The real gems are often hidden in the tiny side-alleys branching off the main path; don't be afraid to get lost.\n\nNo Actual Cats: The name is metaphorical (referring to \"cool cats\" or the street's narrowness). If you want real felines, head to the cat cafes in Harajuku, as the street itself is mostly for humans (and the occasional stylish dog).\n\nRefining Taste: Locals come here not just to shop, but to \"refine their sensitivity\". It's a place to observe the cutting edge of Japanese fashion and design in a low-pressure environment.",
                        HiddenCost =
                            "Gourmet Premium: Expect to pay a \"Harajuku premium\" for food; a lobster roll can run nearly 1,958¥ (~$13.00 USD).\n\nThe \"Power Spot\" Fee: If you combine your trip with Meiji Jingu (nearby), remember the 500¥ fee for the Inner Garden.\n\nDesigner Vintage: Even at \"discount\" prices, high-end second-hand labels at shops like RAGTAG can still cost hundreds of dollars.\n\nThe Trunk Hotel Bar: Just off the Shibuya end is the Trunk Hotel. It's a beautiful place for a drink, but a single cocktail can cost as much as a small meal elsewhere.",
                        NearbyComplements = new List<string>
                        {
                            "Meiji Jingu Shrine: A 7-minute walk away, offering a spiritual and natural contrast to the fashion hub.",
                            "Yoyogi Park: The perfect spot to sit and eat your take-out snacks from Cat Street.",
                            "Miyashita Park: The Shibuya end of the street leads directly to this modern rooftop park and shopping complex.",
                            "Omotesando: The high-fashion boulevard that intersects Cat Street, home to the world's major luxury houses.",
                        },
                        BestTimeToVisit =
                            "Spring/Autumn: The walk is entirely outdoors, so mild weather is key.\n\nWeekday Afternoons: To enjoy the relaxed vibe without the larger weekend crowds.\n\nGolden Hour: The street art and glass-fronted buildings look spectacular as the sun sets over the low-rise neighborhood.",
                        crowdLevel =
                            "Moderate (5/10): Busy enough to feel vibrant, but rarely as claustrophobic as Takeshita Street.",
                        Accessibility =
                            "Rating: 9/10\n\nThe Terrain: Being a mostly pedestrian-only promenade, it is very easy to navigate on foot, though some smaller boutiques may have narrow entrances or stairs.",
                        IdealDuration =
                            "1 to 3 Hours\n\nThis allows time for a full end-to-end walk, some boutique browsing, and a coffee or snack break.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 100,
                    MinCost = 10,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = catStreetCatIds,
                    TagIds = catStreetTagIds,
                },
                // 4. Shibuya Crossing
                new DestinationDto
                {
                    DestinationName = "Shibuya Crossing",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/shibuya_crossing.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Have you ever seen a thousand people walk directly at each other and wondered why nobody ever collides?\n\nShibuya Crossing is a miracle of \"organized chaos.\" It is the busiest intersection on Earth, where five major roads meet in a rhythmic \"scramble.\" Every two minutes, the traffic lights turn red, the giant screens dim their roar, and a human tide surges from every corner. It is the visual heartbeat of Tokyo—a neon-drenched proof that millions of people can move in perfect, silent synchronization.\n\nThis isn't just a road; it's a stage. It has been the backdrop for everything from The Fast and the Furious to Lost in Translation. To stand in the middle of it as the lights change is to feel the sheer, kinetic electricity of modern Japan.\n\nWhat to Do:\n\nThe Scramble Walk: Experience the \"heart of the storm\" by crossing diagonally. It's a rite of passage for every first-time visitor.\n\nHachiko Statue: Visit the bronze statue of Japan's most loyal dog, located just outside the station exit. It's the city's most famous meeting spot.\n\nThe View from Above: Watch the human \"ant farm\" from an elevated position (see Hidden Cost for details on the best decks).\n\nCenter Gai Exploration: After crossing, dive into this pedestrian street the epicenter of Shibuya's youth culture, packed with ramen shops, game centers, and fashion boutiques.\n\nNeon Photography: Visit at night when the massive LED screens turn the entire intersection into a glowing, futuristic canyon.",
                        Directions =
                            "Best Station: Shibuya Station (JR Yamanote, Saikyo, Hanzomon, Ginza, and Fukutoshin Lines).\n\nKey Exit: The Hachiko Exit. Follow the yellow signs inside the station—if you take the wrong exit, you might end up blocks away.\n\nThe Direction Walk: Once you step out of the Hachiko Exit, the crossing is immediately in front of you. You can't miss it.\n\nAddress for Taxi: 東京都渋谷区道玄坂2-2-1 (This will drop you right at the edge of the crossing near the station).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Crossing the street is 100% free.\n\nPeak Hours: The heaviest \"scramble\" happens during the evening rush hour (5:00 PM – 8:00 PM) and on Friday/Saturday nights.\n\nAccessibility: The crossing is fully wheelchair and stroller accessible with flat pavement and clear tactile paving for the visually impaired.\n\nPublic Etiquette: Eating while walking through the crossing is generally looked down upon. Save your snacks for the standing areas near the food stalls.",
                        ThingsToBeWaryOf =
                            "The \"Scramble\" Rush: Don't stop in the dead center of the crossing for a long photoshoot; you will block the flow. Snap quickly and keep moving.\n\nScams: Occasionally, \"friendly\" individuals may approach you offering to take you to a \"cool bar\" or \"free party.\" These are almost always overpriced \"bottling\" scams. Stick to reputable places.\n\nPickpockets: While safety is a 10, the extreme density during rush hour is the only time you should be mindful of your belongings in Tokyo.\n\nStation Construction: Shibuya Station is a perpetual construction site. Signage changes often, so look for the \"Hachiko\" icons rather than memorizing a specific hallway.",
                        LocalPerspective =
                            "The Rainy Day \"Magic\": Locals know that the crossing looks its best when it rains. The sea of transparent umbrellas reflecting the neon lights creates a \"Cyberpunk\" aesthetic that is perfect for photography.\n\nCommuter Awareness: While it's a playground for tourists, remember that for thousands of people, this is just their morning commute. Try not to stop dead in the middle of the street to take a selfie; keep moving with the flow.\n\nMeeting Point Etiquette: Don't just say \"meet at Hachiko.\" It's so crowded you'll never find your friends. Pick a specific shop entrance nearby, like the \"Moyai Statue\" on the opposite side of the station.",
                        HiddenCost =
                            "Shibuya Sky (Observation Deck): For the ultimate 360-degree view. Tickets in 2026 are approximately 2,500¥ – 3,500¥ ($17 - $23 USD). Sunset slots must be booked weeks in advance.\n\nMagnet by Shibuya 109 (Crossing View): A rooftop deck on the 8th floor. Entry is roughly 1,800¥ ($12 USD) and usually includes one drink. It's the best \"front-row\" seat for overhead photos.\n\nThe \"Starbucks Tax\": The Starbucks in the QFRONT building is famous for its view. It's now a \"reimagined\" store where you'll likely need to wait in line and purchase a drink (~600¥+) just to get near the window.\n\nHachiko Family Photos: There is no fee to see the dog, but there is often a long line of tourists. If you want a photo with the statue, expect to \"pay\" with 15 minutes of your time.",
                        NearbyComplements = new List<string>
                        {
                            "Shibuya 109: The iconic cylindrical fashion building for \"Gal\" and youth trends.",
                            "Miyashita Park: A 5-minute walk. A modern rooftop park with a skate park, bouldering wall, and high-end shopping.",
                            "Dogenzaka: The hill behind the crossing, home to Tokyo's most famous nightclubs and \"Love Hotel Hill.\"",
                            "Shibuya Hikarie: A sophisticated skyscraper with a free 11th-floor sky lobby and art exhibitions.",
                        },
                        BestTimeToVisit =
                            "Friday Night (7:00 PM): For the absolute maximum number of people and the brightest neon.\n\nRainy Evenings: For the umbrella-ocean photography.\n\nSunday Morning (8:00 AM): If you want to see the crossing strangely empty and eerie—a rare sight.\n\nSunday Afternoons: For a mix of tourists and stylish locals out for \"Nichiyoubi\" (Sunday) shopping.",
                        crowdLevel = "Extreme (10/10): It is the definition of a crowd.",
                        Accessibility =
                            "Rating: 9/10\n\nThe Terrain: The pavement is smooth and level with curb cuts. Shibuya Station is fully equipped with elevators and ramps. However, the sheer density of people during rush hour can be physically overwhelming for wheelchair users or those with sensory sensitivities.",
                        IdealDuration =
                            "30 to 60 Minutes\n\nEnough time to cross twice, take photos of Hachiko, and grab a coffee to watch the cycle from a window.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 5,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = crossingCatIds,
                    TagIds = crossingTagIds,
                },
                // 5. Shibuya Sky
                new DestinationDto
                {
                    DestinationName = "Shibuya Sky",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/shibuya_sky.webp",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Imagine standing 229 meters above one of the densest cities on Earth, with nothing but a waist-high glass wall between you and the horizon. Shibuya Sky is more than just a view; it's an open-air \"sky stage\" on the rooftop of the Shibuya Scramble Square building. The experience begins before you even reach the top, with a high-tech elevator ride featuring 3D sound and light art. Once you step out onto the 47th floor, you're treated to a panoramic sweep that includes the Tokyo Skytree, Tokyo Tower, the Imperial Palace, and—on clear days—the majestic silhouette of Mt. Fuji. It is the ultimate vantage point to watch the \"organized chaos\" of the Scramble Crossing from a peaceful, wind-swept height.\n\nWhat to Do:\n\nSky Edge: Stand at the corner where the glass is lowest for the iconic \"flying over the city\" photo.\n\nThe Glass Escalator: Take a video as you ascend to the roof; it's widely considered one of the most Instagrammable spots in Tokyo.\n\nCloud Hammock: Lie back on the rooftop netting and look straight up at the sky while the city hums far below.\n\nGeo Compass: Use the floor-embedded compass to identify landmarks like the Pacific Ocean or distant mountain ranges.\n\nSky Gallery: If the wind gets too strong, head to the 46th floor for indoor digital art installations and the \"Time River\" window walk.",
                        Directions =
                            "Best Station: Shibuya Station (directly connected).\n\nKey Exit: Look for the East Exit or signs specifically for Shibuya Scramble Square.\n\nThe Direction Walk: Entrance is on the 14th Floor. Take the dedicated elevators from the ground floor of Scramble Square to the ticket counter.\n\nAddress for Taxi: 〒150-0043 東京都渋谷区道玄坂2 (Shibuya Scramble Square).",
                        WhatToKnow =
                            "Booking Strategy: Always book online in advance. It's cheaper (approx. 2,700¥ vs 3,000¥) and time slots—especially sunset—frequently sell out weeks ahead.\n\nWeather Policy: The rooftop (\"Sky Stage\") may close due to high winds or rain. If this happens, you can still access the indoor 46th-floor gallery, but no refunds are given.\n\nStrict Safety: You cannot take hats, scarves, tripods, selfie sticks, or loose bags to the roof. Everything must go in a 100¥ returnable locker on the 46th floor. Only cameras with neck straps and phones are permitted.\n\nSunset Timing: For your visit today (April 29), sunset is at approximately 6:25 PM. Aim for a 5:40 PM or 6:00 PM entry slot to catch the full transition.",
                        ThingsToBeWaryOf =
                            "The Wind Chill: Even on a warm day, it is significantly colder and windier at 229 meters. Bring a light jacket with pockets (since you can't bring a bag).\n\nTicket Scarcity: If adult tickets are sold out online, they are not available at the counter. Don't risk showing up without a reservation for sunset.\n\nNo Re-entry: Once you leave the observation area, you cannot go back in. Make sure you've taken all your photos before heading to the souvenir shop.",
                        LocalPerspective =
                            "The Golden 30: Locals aim to book their entry slot exactly 30 minutes before sunset. This allows you to see the city in daylight, catch the \"Blue Hour\" glow, and watch the neon lights of Tokyo flicker to life.\n\nWinter Clarity: Visit in the colder months (December–February) if you want the best chance of seeing Mt. Fuji; the air is much clearer and less humid then.\n\nThe \"Secret\" Bar: There is a rooftop bar (Paradise Lounge) where you can grab a drink and enjoy the view without the crowds often found at the photo corners.",
                        HiddenCost =
                            "The Locker Fee: You'll need a 100¥ coin for the mandatory lockers (though you get it back when you return the key).\n\nProfessional Photos: Staff at \"Sky Edge\" offer to take your photo with their pro gear. While they'll use your phone for one shot, the high-res printed version costs around 1,500¥.\n\nPeak Hour Surcharge: Note that after 3:00 PM, online ticket prices increase to 3,400¥ due to the high demand for sunset views.",
                        NearbyComplements = new List<string>
                        {
                            "Paradise Lounge: A retro-futuristic music bar on the 46th floor for snacks and cocktails with a view.",
                            "Shibuya Scramble Square Shops: The lower floors house some of Tokyo's most high-end patisseries and boutiques.",
                            "Hachiko Statue: Just a 3-minute walk from the ground floor entrance.",
                        },
                        BestTimeToVisit =
                            "Sunset (Approx. 6:25 PM today): For the most dramatic lighting and city views.\n\n10:00 AM (Opening): If you want the \"Sky Edge\" photo without waiting in a 30-minute queue.\n\nLate Evening (9:00 PM): For a romantic, quiet atmosphere and the \"Crossing Light\" searchlight display.",
                        crowdLevel =
                            "High (9/10): It is currently one of the most popular attractions in Japan. Expect queues for the best photo corners.",
                        Accessibility =
                            "Rating: 10/10\n\nThe Terrain: Excellent. The entire facility is wheelchair and stroller accessible with wide elevators and flat surfaces.\n\nFacilities: Accessible restrooms are available on the 46th floor near the lockers.",
                        IdealDuration =
                            "90 Minutes\n\nThis gives you time for the locker transition, the rooftop walk, and browsing the Sky Gallery art.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 18,
                    MinCost = 14,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = shibuyaSkyCatIds,
                    TagIds = shibuyaSkyTagIds,
                },
                // 6. Senso-ji Temple
                new DestinationDto
                {
                    DestinationName = "Senso-ji Temple (Asakusa Kannon)",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/sensoji.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Welcome to the spiritual soul of Tokyo. Founded in 645 AD, Senso-ji is the city's oldest temple, born from a legend of two brothers who fished a golden statue of Kannon (the Goddess of Mercy) out of the Sumida River. Even when they threw it back, it returned—a sign that a sanctuary must be built.\n\nToday, Senso-ji is a vibrant explosion of crimson red, incense smoke, and centuries-old tradition. It serves as a bridge between the hyper-modernity of Tokyo and the \"Shitamachi\" (old downtown) atmosphere of the Edo period. Whether you are here to pray, shop, or simply marvel at the architecture, it remains the most culturally significant stop in the capital.\n\nWhat to Do:\n\nPass through Kaminarimon: Walk under the massive 700kg red lantern at the \"Thunder Gate.\" Look under the lantern to see a beautifully carved dragon.\n\nThe Nakamise Sprints: Wander the 250-meter shopping street. Try Age-manju (fried bean cakes) or Ningyo-yaki (small cakes shaped like local landmarks).\n\nPurify at the Jokoro: Join the locals at the large incense burner. Waft the smoke over parts of your body you wish to heal or improve (heads are popular for wisdom!).\n\nDraw an Omikuji: For 100¥, shake a metal tin for a fortune. If you get \"Bad Luck,\" tie it to the nearby wire rack to leave the misfortune at the temple.\n\nEvening Walk: Visit after 6:00 PM. While the Main Hall closes, the gates and pagoda are illuminated, and the crowds vanish, revealing a cinematic, quiet beauty.\n\nOffer a Prayer: At the Main Hall, toss a coin (5 yen is traditional), bow twice, clap twice, and pray with palms together.\n\nAdmire the Pagoda: View the Five-Storied Pagoda, where each level represents an element: earth, water, fire, wind, and sky.",
                        Directions =
                            "Best Station: Asakusa Station (Tokyo Metro Ginza Line, Toei Asakusa Line, and Tobu Railway).\n\nKey Access Point: Take Exit 1 or Exit 3 and walk 2 minutes toward the giant red gate (Kaminarimon).\n\nThe Direction Walk: The temple is just a few steps from the station exits; follow the signs for the Kaminarimon Gate.\n\nAddress for Taxi: 東京都台東区浅草2-3-1 (Senso-ji Temple).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Entry to the grounds is Free.\n\nMain Hall Hours: 6:00 AM – 5:00 PM (Opens at 6:30 AM from Oct–Mar). The grounds themselves never close.\n\nPrayer Etiquette: Toss a coin (5¥ is lucky), bow twice, clap twice, pray silently, and bow once more.\n\nThe \"Hidden\" Statue: The original Kannon statue found in 628 AD is still there, but it is a \"Hibutsu\" (Hidden Buddha)—it is never shown to the public, not even to the monks!\n\nGrounds Access: The temple grounds themselves never close, making it accessible for late-night strolls.\n\nCrowds: As one of the most popular sites in Japan, it is almost always busy, especially during festivals like the Sanja Matsuri in May.\n\nPro-Tip: Looking for a specific blessing? Senso-ji is a \"Sho Kannon\" temple, meaning the deity here is specifically focused on relieving immediate, worldly suffering. It's the place to pray for things happening right now in your life!",
                        ThingsToBeWaryOf =
                            "Mid-Day Crowds: Between 11:00 AM and 3:00 PM, Nakamise Street can be overwhelming. If you are claustrophobic, aim for an 8:00 AM arrival.\n\nThe \"Bad Luck\" Myth: Senso-ji is famous for having a high ratio of \"Bad Luck\" fortunes. Don't take it personally—it's just the tradition of this specific temple!\n\nRickshaw Prices: The \"Shafu\" (rickshaw pullers) are friendly and offer great tours, but they are expensive (approx. 4,000¥ - 9,000¥ depending on duration). Agree on the price before you sit down.",
                        LocalPerspective =
                            "The \"Secret\" Garden: Most tourists miss Denboin Garden, a tranquil pond garden tucked behind the temple. It is only open to the public during specific weeks in spring — check local flyers!\n\nGoshuin (Temple Seal): If you have a Goshuincho (stamp book), head to the Yogodo Hall to the left of the main building. It is one of the most beautiful calligraphy seals in Tokyo.\n\nBeyond the Main Hall: Explore the quieter areas to the left of the hall to find peaceful stone lanterns, small shrines for weather deities, and the Peace Monument.\n\nAvoid the Main Path: For a more local feel, walk parallel to Nakamise Street on the side alleys. You'll find better coffee and authentic craft shops without the shoulder-to-shoulder crowds.",
                        HiddenCost =
                            "Omamori (Amulets): These beautiful silk charms for health, safety, or love cost 500¥ – 1,500¥.\n\nCandle Offerings: You can buy a candle or additional incense for a few hundred yen to offer at the smaller shrines.\n\nMochi Tradition: Legend says eating a snack at Nakamise ensures you will return to Japan. Budget about 500¥ for a variety of treats.\n\nGoshuin: If you collect temple stamps, there is usually a small fee at the Yogodo Hall.",
                        NearbyComplements = new List<string>
                        {
                            "Asakusa Culture Tourist Information Center: Go to the 8th-floor observation deck (Free!) for a bird's-eye view of the temple complex and Tokyo Skytree.",
                            "Hoppy Street: A 3-minute walk away. Famous for outdoor drinking stalls serving \"stew\" and Hoppy (a beer-like beverage).",
                            "Samurai Ninja Museum Tokyo: A 5-minute walk for a hands-on experience with swords and armor.",
                        },
                        BestTimeToVisit =
                            "8:00 AM: You get to see the shopkeepers opening their shutters, which often have beautiful murals painted on them.\n\n7:00 PM: The \"Golden Hour\" for photography, when the red architecture glows against the dark sky.\n\nMay (Sanja Matsuri): If you want to experience one of Tokyo's wildest and largest festivals.\n\nJuly 9–10: During the Shiman-rokusen-nichi festival, when a single visit is said to equal 46,000 prayers.",
                        crowdLevel =
                            "Extreme (10/10): On weekends and holidays.\n\nModerate (4/10): On weekday early mornings.",
                        Accessibility =
                            "Rating: 8/10\n\nThe Terrain: Mostly flat and paved. There is an elevator located to the left of the Main Hall stairs, allowing wheelchair and stroller access to the altar area.",
                        IdealDuration =
                            "1.5 to 2 Hours\n\nEnough time to walk the shops, offer a prayer, and explore the smaller side shrines and gardens.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 15,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = sensojiCatIds,
                    TagIds = sensojiTagIds,
                },
                // 7. Kaminarimon Gate
                new DestinationDto
                {
                    DestinationName = "Kaminarimon Gate",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/kaminarimon.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "If Tokyo had a front door, this would be it. Kaminarimon (Thunder Gate) is the massive, vermilion-red gateway that guards the entrance to Senso-ji Temple. Originally built in 942 by a military commander, it has survived fires, wars, and centuries of change to become the definitive symbol of Asakusa and, arguably, Tokyo itself.\n\nThe gate is a masterclass in religious symbolism, housing two fierce Shinto guardians in the front and two Buddhist deities in the back. It serves as the threshold where the mundane world ends and the sacred journey toward the Kannon statue begins.\n\nWhat to Do:\n\nAdmire the Chōchin: Observe the massive central red lantern, which stands 3.9 meters high, 3.3 meters wide, and weighs 700 kilograms.\n\nInspect the Statues: View the Shinto gods guarding the front: Fūjin (wind god) on the right and Raijin (thunder god) on the left.\n\nMeet the Guardians: Front: Fujin (Wind God) on the right and Raijin (Thunder God) on the left. They protect the temple from natural disasters.\n\nBack: Look at the reverse side of the gate to see Tenryu (Heavenly Dragon) and Kinryu (Golden Dragon) in human form.\n\nLook Up: While walking beneath the giant lantern, look at the bottom to find an intricate carved dragon.\n\nPhotography: Capture the gate's striking red facade, especially at night when the crowds are smaller and the structure is illuminated.",
                        Directions =
                            "Best Station: Asakusa Station.\n\nKey Lines: Ginza Line, Asakusa Line, and Tobu Railway.\n\nThe Direction Walk: The gate is located directly at the entrance to the Senso-ji temple complex and marks the start of Nakamise-dori.\n\nAddress for Taxi: 東京都台東区浅草1-2-3 (Kaminarimon Gate).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Passing through is Free.\n\nDimensions: The gate stands 11 meters high and 11 meters wide.\n\nOfficial Entrance: This is the outer gate; an inner gate (Hozomon) is located further down the path.\n\nCrowd Navigation: Locals know that the area directly under the gate is a bottleneck. For the best \"clean\" photo, stand across the street at the Asakusa Culture Tourist Information Center or use a long lens from the sidewalk.\n\nHistorical Resilience: Although it has stood for over eleven centuries in name, the current iteration is a modern reconstruction designed to showcase ancient beauty.\n\nPro-Tip: If you happen to be there when it's very windy, look at the giant lantern. Despite its 700kg weight, it will sometimes sway just a few inches, making the \"Wind and Thunder\" theme of the gate feel very real!",
                        ThingsToBeWaryOf =
                            "Heavy Crowds: As a primary tourist landmark, it is often extremely congested during peak daylight hours.\n\nRickshaw Solicitations: The area right in front of the gate is \"base camp\" for rickshaw pullers. They are generally very polite, but they will approach you. A simple \"No thank you\" (or Kekkou desu) works fine.",
                        LocalPerspective =
                            "A Symbol of Gratitude: The 1960 reconstruction was donated by Konosuke Matsushita, the founder of Panasonic.\n\nGateway to Culture: Beyond its religious significance, it acts as a traditional welcome to all who enter the sacred grounds.\n\nFolded Lanterns: During the Sanja Matsuri festival in May, the giant lantern is actually collapsed/folded up so that the massive portable shrines (mikoshi) can pass underneath without hitting it!\n\nHidden Dragon: Look at the bottom of the giant lantern at Kaminarimon to find an intricately carved wooden dragon for extra luck.\n\nThe Panasonic Connection: If you look at the bottom of the giant lantern, you will see a bronze nameplate for Matsushita Denki (Panasonic). The founder, Konosuke Matsushita, donated the gate's reconstruction in 1960 after recovering from an illness he prayed away at Senso-ji.",
                        HiddenCost =
                            "100¥ Fortune: Just past the gate, you'll see the first stalls for Omikuji. It's a small \"cost\" for a classic experience.\n\nSnack Temptation: The smells from the first few shops on Nakamise-dori (right behind the gate) are designed to make you spend your 500¥ coins immediately!",
                        NearbyComplements = new List<string>
                        {
                            "Nakamise-dori: The bustling shopping street immediately following the gate.",
                            "Senso-ji Temple: The final destination at the end of the path.",
                            "Asakusa Culture Tourist Information Center: Directly across the street. Go to the 8th-floor balcony for the best overhead photo of the gate and the temple path.",
                            "Kamiya Bar: A 2-minute walk away. Tokyo's oldest Western-style bar, famous for \"Denki Bran\" (Electric Brandy).",
                        },
                        BestTimeToVisit =
                            "7:00 AM: To see the gate without a single tourist in your shot.\n\nSunset: When the red paint glows deep crimson in the \"Golden Hour\" light.\n\nLate Night: For a peaceful, spiritual atmosphere once the illumination kicks in.",
                        crowdLevel =
                            "High (10/10): Often one of the most crowded spots in Tokyo during the day.",
                        Accessibility =
                            "Rating: 9/10\n\nTerrain: The area around the gate is flat and paved, making it easily accessible for pedestrians and those with mobility aids.",
                        IdealDuration =
                            "15 Minutes\n\nJust long enough to admire the statues, find the dragon, and take your iconic \"I'm in Tokyo\" photo.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 1,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = kaminarimonCatIds,
                    TagIds = kaminarimonTagIds,
                },
                // 8. Asakusa Streets
                new DestinationDto
                {
                    DestinationName = "Asakusa Streets",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/asakusa.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "If you want to feel the heartbeat of \"Old Tokyo,\" you go to Asakusa. This is the center of Tokyo's Shitamachi (low city), where the atmosphere of the Edo period still lingers in the smoke of incense and the narrow, winding back alleys. While the towering Tokyo Skytree looms just across the river as a symbol of the future, Asakusa remains anchored by Senso-ji, the city's oldest temple.\n\nBeyond the famous gates, Asakusa is a maze of discovery. It's a place where you can find Japan's oldest amusement park, watch a traditional comedy performance, or eat at a tempura shop that has been using the same recipe for over a century.\n\nWhat to Do:\n\nThe View from Above: Start at the Asakusa Culture Tourist Information Center. Take the elevator to the 8th-floor observation deck (Free!) for a stunning view of the temple complex and the Skytree.\n\nTanuki Street Scavenger Hunt: Duck into this quirky alley to find 12 different statues of Tanuki (raccoon dogs). Rub their bellies for different types of luck!\n\nEat Your Way Through Nakamise: Try Ningyo-yaki (doll-shaped cakes) and fresh Senbei (rice crackers). Pro-tip: Never walk and eat; finish your snack at the stall.\n\nThe \"Golden Turd\" Perspective: Walk toward the Sumida River to see the Asahi Beer Headquarters. The golden flame (officially the \"Asahi Flame\") is a local landmark with a very \"colorful\" nickname.\n\nRickshaw Journey: Take a \"Jinrikisha\" ride. The pullers are incredible storytellers and will take you to hidden spots you'd never find on a map.",
                        Directions =
                            "Best Station: Asakusa Station (Served by Ginza Line, Asakusa Line, and Tobu Railway).\n\nAccess: From Tokyo Station, take the JR Yamanote to Kanda, then transfer to the Ginza Line.\n\nThe Direction Walk: Most major attractions, including Senso-ji and Nakamise, are a short 3–7 minute walk from the station exits.\n\nAddress for Taxi: 東京都台東区浅草2-3-1 (This is the address for Senso-ji, the central landmark).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Street snacks usually cost 150¥ - 500¥.\n\nDon Quijote (Donki): The Asakusa branch is massive. It's the ultimate place for weird Japanese snacks, skincare, and \"sumo wrestler socks.\"\n\nSumida River Boat: You can take a futuristic boat (the Himiko or Hotaluna) from Asakusa Pier to Odaiba for a unique view of the city skyline.\n\nPublic Holidays: Many shops on Kappabashi (Kitchen Street) are closed on Sundays and national holidays.\n\nPro-Tip: If your feet are \"toast\" after walking, look for a local Sento (public bath). There are several near the Hanayashiki amusement park where you can soak with the locals for about 500¥.",
                        ThingsToBeWaryOf =
                            "Nakamise Crowds: The main street is a bottleneck. If you feel overwhelmed, use the parallel side streets; they have equally charming shops but 80% fewer people.\n\nEarly Closures: Unlike Shibuya, Asakusa goes to bed early. Most shops on Nakamise close by 5:00 PM or 6:00 PM.\n\nPublic Trash Bins: They are nearly non-existent. Carry a small plastic bag in your daypack to hold your trash until you get back to your hotel.\n\nSensory Overload: Massive stores like Don Quijote are chaotic and loud; consider earplugs if sensitive to noise.",
                        LocalPerspective =
                            "The \"Hidden\" Senso-ji: The back garden of the temple near the pagoda is significantly quieter and offers a peaceful escape from the Nakamise crowds.\n\nHoppy Street: Known to locals as \"Nikomi Street,\" this is the place to go for outdoor drinking and stewed beef tendons. It's where the locals unwind after a day of work.\n\nThe \"3 Coins\" Hack: If you need souvenirs but are on a budget, look for the 3 Coins store in the nearby mall for high-quality, 300-yen Japanese home goods.\n\nThe \"Golden Turd\": Locals have colorful nicknames for the \"Asahi Flame\" sculpture atop the Asahi Super Dry Hall next to the river.",
                        HiddenCost =
                            "Rickshaw Tours: These are premium experiences. Expect to pay around 9,000¥ ($60) for a 30-minute tour for two people.\n\nMelon Bread Ice Cream: It's delicious, but at 600¥-800¥, it's a \"tourist-priced\" treat.\n\nAsahi Sky Room: A beer with a view on the 22nd floor of the Asahi building will cost you about 800¥-1,200¥.\n\nAmusement Park: Hanayashiki requires a 1,200 yen entry fee plus separate costs for individual rides.\n\nShopping Bags: Most shops in Japan now charge for plastic bags, so carrying a reusable one is recommended.",
                        NearbyComplements = new List<string>
                        {
                            "Kappabashi Street: The place to buy professional kitchenware and famous plastic food samples.",
                            "Sumida Park: A riverside park perfect for viewing cherry blossoms in the spring.",
                            "Tokyo Skytree: A 20-minute walk across the river for high-altitude observation decks.",
                        },
                        BestTimeToVisit =
                            "7:30 AM: To see the \"Shutter Art.\" Before the shops open, the metal shutters of Nakamise are painted with scenes of Japanese history and festivals.\n\nLate July: For the Sumida River Fireworks Festival, one of the biggest and oldest displays in Japan.\n\nSunset: When the lights of the Skytree turn on and reflect off the river.",
                        crowdLevel = "High (9/10): Especially during midday and on weekends.",
                        Accessibility =
                            "Rating: 9/10\n\nAsakusa is very flat. Most major attractions and the main temple hall have elevators. Even the oldest underground shopping street is accessible via the station lifts.",
                        IdealDuration =
                            "4 to 6 Hours\n\nThis gives you time for the temple, a leisurely lunch on Hoppy Street, some shopping at Donki, and a walk along the river.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 30,
                    MinCost = 10,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = asakusaStreetsCatIds,
                    TagIds = asakusaStreetsTagIds,
                },
                // 9. Sumida Riverwalk
                new DestinationDto
                {
                    DestinationName = "Sumida Riverwalk",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/riverwalk.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "If the Sumida River could talk, it would tell the story of Tokyo's transformation from the marshy fishing village of Edo to a global megacity. Flowing through the heart of the \"Shitamachi\" (low city), this 15-mile waterway is more than just a geographic boundary; it is the cultural soul of the city.\n\nHistorically, it was the backdrop for Ukiyo-e woodblock prints and Kabuki theater. Today, it serves as a stunning visual corridor where the ancient temples of Asakusa on the west bank gaze across the water at the 634-meter TOKYO SKYTREE on the east. Whether you are crossing the futuristic Sumida River Walk or cruising in a traditional yakatabune (wooden boat), the river offers a sense of space and sky that is rare in the cramped streets of Tokyo.\n\nWhat to Do:\n\nThe Sumida River Walk: Stroll across this 160-meter pedestrian bridge that links Asakusa to the Skytree area. Look for the glass floor panels to see the water and boats passing beneath your feet.\n\nTokyo Mizumachi: Explore this sleek commercial complex built under the railway tracks. It features riverside terraces, a bouldering gym (Lattest Sports), and trendy cafes like LAND_A.\n\nSumida River Terrace: Walk along the stone-paved paths that line both sides of the river. It's a 2-hour journey if you walk the whole way, passing 16 unique bridges, each with its own color and architectural style.\n\nWater Bus Cruise: Catch a futuristic boat from Asakusa Pier. The Himiko or Hotaluna (designed by manga artist Leiji Matsumoto) look like spaceships and travel all the way to Odaiba.\n\nCherry Blossom Spotting: Visit Sumida Park in the spring. Thousands of trees line the banks, creating a \"tunnel\" of pink blossoms with the Skytree in the background—a photographer's dream.\n\nCultural Exploration: Visit the nearby Kokugikan (Sumo Hall) in Ryogoku or the Edo-Tokyo Museum to see how the river's landscape has evolved over 400 years.",
                        Directions =
                            "Best Station: Asakusa Station (Tokyo Metro Ginza Line, Toei Asakusa Line, or TOBU SKYTREE Line).\n\nKey Exit: From Tokyo Metro Ginza Line: Take Exit 5 (7-min walk to the river). From TOBU SKYTREE Line: Take the North Exit (3-min walk to the river).\n\nThe Direction Walk: From Asakusa Station, head toward the Azuma Bridge to begin your riverside stroll. To reach the Tokyo Skytree area, follow the Sumida River Walk bridge located alongside the railway tracks.\n\nAddress for Taxi: 東京都台東区花川戸1-1 (Asakusa Pier / Sumida Park area).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥).\n\nWalking Hours: The Sumida River Walk is open from 7:00 AM to 10:00 PM.\n\nWater Bus Fare: A trip from Asakusa to Odaiba is typically around 2,000¥ (~$13.50 USD).\n\nMuseum Note: The Edo-Tokyo Museum (near Ryogoku Bridge) is a local favorite but is currently under renovation and expected to reopen in Spring 2026.\n\nFireworks Tradition: The Sumida River Fireworks Festival (late July) has been running since 1733 and is the oldest display in Japan.\n\nDining Etiquette: Many restaurants in Tokyo Mizumachi offer riverside terraces; \"LAND_A\" is a popular choice for waterfront dining.\n\nPro-Tip: If you're walking the Sumida River Walk, look for the \"Solakara-chan\" (Skytree mascot) hidden somewhere on the bridge structure. It's a fun little \"easter egg\" for eagle-eyed visitors!",
                        ThingsToBeWaryOf =
                            "The Three Cs: During major events like the fireworks or cherry blossom peak, avoid the \"Three Cs\": Confined spaces, Crowded places, and Close contact. The riverbanks can become incredibly packed.\n\nWeather Exposure: The riverside has very little shade or cover. On hot summer days or rainy afternoons, the walk can be punishing. Stay hydrated!\n\nBridge Distances: Bridges are about 5–10 minutes apart on foot. If you start the 2-hour walk, be sure you have the energy to finish or reach a station!\n\nVarying Hours: Operating hours for stores in Tokyo Mizumachi and nearby malls vary significantly by floor and facility.",
                        LocalPerspective =
                            "The \"Golden Turd\": On the east bank, you'll see the Asahi Super Dry Hall with its famous golden squiggle. While officially called the \"Asahi Flame,\" locals affectionately (and cheekily) refer to it as the \"Golden Turd.\"\n\nMimeguri-jinja Shrine: A hidden gem on the east bank. It houses the protective deity of the Mitsui clan (founders of Mitsukoshi department stores) and features unique lion statues.\n\nNight Illumination: The Sumida River Walk and the Skytree are synchronized in their lighting. The colors change based on the seasons and special holidays—sunset is the best time to see the transition.\n\nNostalgic Alleys: Venture into the streets of Mukojima near the river to find historic shrines like Mimeguri-jinja or listen for geisha practicing shamisen on Kenban-dori.",
                        HiddenCost =
                            "Riverside Dining: While the walk is free, dining on a \"Kawaterasu\" (river terrace) at Mizumachi or near Azuma Bridge often comes with a higher price tag for the view.\n\nSumo Museum: Entry is free, but tickets for grand tournaments in January, May, and September must be purchased well in advance.\n\nMizumachi Experiences: Activities like bouldering at Lattest Sports or staying at WISE OWL HOSTELS will require separate bookings/fees.\n\nYakatabune Dinner: If you choose a private dinner cruise on a traditional boat, expect to pay 10,000¥ - 15,000¥ (~$67 - $100 USD) per person.",
                        NearbyComplements = new List<string>
                        {
                            "Tokyo Skytree Town: Just across the River Walk, featuring the observatory, Sumida Aquarium, and the massive Solamachi mall.",
                            "Senso-ji Temple: Only a few minutes' walk from the river's west bank.",
                            "Ryogoku Kokugikan: The national Sumo hall, located further south along the river (about a 20-minute stroll or one train stop).",
                            "Sumida Park: A prime location for photography, framing the river with the Skytree in the background.",
                            "Kinshicho Area: A short walk or train ride away, featuring Kinshi Park and the Sumida Triphony Hall.",
                        },
                        BestTimeToVisit =
                            "Sunset: To see the bridges and the Skytree light up simultaneously.\n\nLate March/Early April: For the world-famous cherry blossoms in Sumida Park.\n\nLast Saturday of July: For the Fireworks Festival (if you can handle extreme crowds).",
                        crowdLevel =
                            "Low to Moderate (3/10): On weekday mornings along the River Terrace.\n\nExtreme (10/10): During the Cherry Blossom season and the Fireworks Festival.",
                        Accessibility =
                            "Rating: 9/10\n\nThe Terrain: Most of the river walk and the \"Sumida River Terrace\" are paved, flat, and wheelchair/stroller friendly. The Sumida River Walk bridge is specifically designed for easy pedestrian access.",
                        IdealDuration =
                            "45 Minutes to 2 Hours\n\n45 minutes for a quick cross from Asakusa to Skytree; 2 hours for a full scenic stroll along the terrace to Ryogoku.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.JulToSep,
                    },
                    MaxCost = 10,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = riverwalkCatIds,
                    TagIds = riverwalkTagIds,
                },
                // 10. Shinjuku Gyoen
                new DestinationDto
                {
                    DestinationName = "Shinjuku Gyoen",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/gyoen.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Imagine a 144-acre emerald sanctuary dropped into the middle of the world's most neon-soaked skyline. Shinjuku Gyoen isn't just a park; it's a living museum of landscape design. Originally the private residence of Lord Naito, it evolved into an Imperial garden before opening to the public in 1949.\n\nThe garden is famous for its unique \"triple threat\" of design styles: a symmetrical French Formal Garden, an open English Landscape Garden, and a tranquil Japanese Traditional Garden. Whether you're looking for a quiet spot to watch turtles in a pond or a vast lawn to view over 1,000 cherry trees, this park provides the ultimate \"reset button\" for city-weary travelers.\n\nWhat to Do:\n\n- Style Hopping: Walk through all three distinct gardens. Don't miss the Taiwan Pavilion (Kyu Goryotei) for a stunning view across the water in the Japanese section.\n\n- The Greenhouse: Visit the modern, eco-friendly greenhouse to see over 2,700 species of tropical and subtropical plants, including rare orchids.\n\n- Hanami (Flower Viewing): In spring, the English Landscape Garden becomes a sea of pink. Because Shinjuku Gyoen has early and late-blooming varieties, the season here lasts longer than elsewhere in Tokyo.\n\n- Teahouse Experience: Stop at the traditional tea room in the Japanese Garden for a bowl of matcha and a seasonal sweet.\n\n- Chrysanthemum Exhibition: If visiting in early November, witness the spectacular floral displays that carry on the traditions of the Imperial era.",
                        Directions =
                            "Best Stations: Shinjuku Gate: 10-min walk from JR Shinjuku Station (New South Exit) or 5-min from Shinjukugyoenmae Station (Marunouchi Line). Okido Gate: 5-min walk from Shinjukugyoenmae Station. Sendagaya Gate: 5-min walk from JR Sendagaya Station.\n\nThe Direction Walk: From Shinjukugyoenmae Station, follow the signs toward the green trees. The park's perimeter is fenced, so look for the designated gate entrances.\n\nAddress for Taxi: 東京都新宿区内藤町11 (11 Naito-machi, Shinjuku-ku).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥).\n\nAdmission: 500¥ (~$3.33 USD) for adults. Children 15 and under are Free.\n\nPayment: Automatic machines take cash, credit cards, and IC cards (Suica/Pasmo).\n\nHours: Vary by season: Winter: 9:00 AM – 4:00 PM (Gates close 4:30 PM). Summer: 9:00 AM – 6:30 PM (Gates close 7:00 PM).\n\nClosed: Mondays (unless it's a holiday, then Tuesday). Open 7 days a week during cherry blossom and chrysanthemum peak seasons.\n\nPro-Tip: If you enter through the Shinjuku Gate, head straight for the Starbucks. It's arguably the most beautiful Starbucks in Japan, built with sustainable wood and featuring floor-to-ceiling windows overlooking the park's central pond.",
                        ThingsToBeWaryOf =
                            "The \"No-Go\" Items: Alcohol, sports equipment (bats/balls), and musical instruments are not allowed. Security at the gates often performs brief bag checks.\n\nLast Entry: Entry stops 30 minutes before the gates close. Don't arrive at 4:15 PM expecting to get in!\n\nWeekend Reservations: During the height of Sakura season (late March), you must book a timed entry slot online in advance. Walk-ins are often turned away on peak Saturdays/Sundays.",
                        LocalPerspective =
                            "The Picnic Hack: Shinjuku Gyoen is one of the few parks in Tokyo where alcohol is strictly prohibited. This makes it the \"locals' choice\" for families and those who want a peaceful, quiet hanami without the rowdy parties found at Ueno or Yoyogi.\n\nE-Ticket Smoothness: Use the Asoview! website to buy an e-ticket in advance. During peak cherry blossom weekends, you can skip the massive ticket machine lines and scan your QR code directly at the gate.\n\nSeasonal Clock: Locals check the \"Shinjuku Gyoen Twitter/X\" for daily flower updates. The park is a \"natural calendar\" for Tokyoites.",
                        HiddenCost =
                            "Annual Passport: At 2,000¥ (~$13.33 USD), it pays for itself in just four visits. If you are staying in Shinjuku for a week, it's worth considering.\n\nTeahouse Matcha: Budget about 700¥ - 1,000¥ for a tea set.\n\nGarden Souvenirs: The museum shop sells high-quality botanical goods and \"Shinjuku Gyoen\" branded sweets that are slightly pricier than standard convenience store snacks.",
                        NearbyComplements = new List<string>
                        {
                            "Shinjuku San-chome: A 10-minute walk away, filled with thousands of tiny restaurants, bars, and department stores like Isetan.",
                            "Tokyo Metropolitan Government Building: A 20-minute walk (or short subway ride) for a free panoramic view of the city.",
                            "Meiji Jingu: A short hop on the JR line from Sendagaya Gate.",
                        },
                        BestTimeToVisit =
                            "9:00 AM: Be there when the gates open to enjoy the Japanese Garden in near-solitude.\n\nEarly November: To see the \"Momijiyama\" (Maple Mountain) on the eastern side glow with autumn colors.\n\nLate March: For the iconic cherry blossom experience.",
                        crowdLevel =
                            "Moderate (5/10): Weekdays.\n\nHigh (9/10): Weekends and blossom season.",
                        Accessibility =
                            "Rating: 10/10\n\nThe Terrain: Extremely flat and paved. Most gravel paths have a paved strip for wheelchairs and strollers. Accessible restrooms are located throughout the park.",
                        IdealDuration =
                            "2 to 3 Hours\n\nThis allows you to walk the full 3.5 km circumference and spend 30 minutes in the greenhouse.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 5,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = gyoenCatIds,
                    TagIds = gyoenTagIds,
                },
                // 11. Tokyo Metropolitan Government Building
                new DestinationDto
                {
                    DestinationName = "Tokyo Metropolitan Government Building",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/tokyo_metropolitan.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Designed by the legendary architect Kenzo Tange, the Tokyo Metropolitan Government Building is much more than an office for bureaucrats. This postmodern masterpiece, meant to resemble both a Gothic cathedral and a computer chip, was the tallest building in Tokyo until 2007.\n\nThe \"Tocho\" is a traveler's favorite because it offers something rare in Tokyo: a world-class, 202-meter-high view for absolutely zero yen. While other landmarks charge high admission fees, this building invites you to its 45th-floor twin towers to see the city sprawl toward the horizon. At night, the building transforms into a canvas for the world's largest projection mapping display, making it a dual-threat destination for both views and visual art.\n\nWhat to Do:\n\nThe Observation Decks: Head to the 45th floor of either the North or South Tower. On clear days (especially winter mornings), you can see Mt. Fuji to the west. At night, the city lights of Shinjuku look like a circuit board below you.\n\nProjection Mapping Show: Stick around in the evenings to witness \"Tokyo Night & Light.\" The building facade becomes a massive screen for a 15-minute high-tech light show, running every 30 minutes.\n\nThe Yellow Piano: In the South Observatory, look for the \"Tocho Omoide Piano.\" It's a beautifully decorated grand piano that visitors are allowed to play, often providing a live soundtrack to your sightseeing.\n\nTourist Information Hub: Visit the 1st and 2nd floors. Not only can you get maps of Tokyo, but there are often fairs showcasing local crafts and snacks from all over regional Japan.\n\nFind the Kumade: Look for the giant decorative bamboo rake on the 2nd floor—a traditional symbol used to \"rake in\" good fortune and business success.",
                        Directions =
                            "Best Station: Tocho-mae Station (Toei Oedo Line) is located directly in the basement of the building.\n\nAlternative: A 10-minute walk from the West Exit of JR Shinjuku Station.\n\nThe Direction Walk: From Shinjuku Station, follow the signs for the \"Moving Walkway\" (Keio Plaza Hotel direction). It's a straight, weather-protected path that leads almost to the front door.\n\nAddress for Taxi: 東京都新宿区西新宿2-8-1 (2-8-1 Nishi-Shinjuku).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥). Admission is Free.\n\nSecurity: There is a mandatory bag check before you enter the dedicated observatory elevators. Prohibited items include flammables and large luggage (use lockers at the station).\n\nHours: Generally 9:30 AM – 10:00 PM (Last entry at 9:30 PM).\n\nClosures: Towers take turns closing on different Mondays/Tuesdays for inspections. Always check the official website or the notice board at the entrance before picking a tower.\n\nPro-Tip: Don't just look out the windows! Look at the floor-to-ceiling photographic panels around the edge of the room. They identify every major building and mountain in view so you don't have to guess what you're looking at.",
                        ThingsToBeWaryOf =
                            "The Queue: Even though it's free, the line for the elevators can be 30–45 minutes long on weekends or during sunset. Try to arrive at least an hour before you want to see the sun go down.\n\nReflection: The glass can have significant glare at night, making photography tricky. To get a good shot, place your lens directly against the glass (or use a lens hood/scarf to block interior light).\n\nWeather: Projection mapping and views are highly dependent on visibility. If it's raining or very foggy, the show may be cancelled and the views will be non-existent.\n\nNorth Tower Variation: On certain days, the North Observatory may close earlier (17:30) if the South tower is also operational.",
                        LocalPerspective =
                            "South vs. North: Locals usually prefer the South Tower for daytime views (better angle for Mt. Fuji) and the North Tower for evening views (it typically stays open later when the South is closed).\n\nThe \"Gov\" Lunch: For a truly local experience, you can actually eat at the Staff Canteen (32nd floor) during lunchtime on weekdays. It's cheap, high-quality, and offers a great view, though it feels a bit like a high school cafeteria.\n\nWinter Clarity: If your goal is to see Mt. Fuji, aim to be at the observatory between 9:30 AM and 10:30 AM in December or January. The cold, dry air makes the mountain pop in the distance.",
                        HiddenCost =
                            "The Gift Shop Trap: The souvenirs in the observatory are actually quite good, featuring limited-edition Tokyo-themed items you won't find at the airport.\n\nObservatory Cafe: A coffee or beer with a view will cost you about 600¥ - 900¥ (~$4.00 – $6.00 USD). It's the \"price\" of sitting by the window.\n\nStaff Canteen Meal: If you opt for the 32nd-floor lunch, expect to pay around 600¥ - 800¥ for a set meal.",
                        NearbyComplements = new List<string>
                        {
                            "Shinjuku Central Park: Located right behind the building, it's a great spot to see the skyscraper from below or let kids run around.",
                            "Park Hyatt Tokyo: The famous \"Lost in Translation\" hotel is just a few blocks away.",
                            "Omoide Yokocho (Piss Alley): A 12-minute walk back toward Shinjuku Station for a gritty, narrow-alley dinner of yakitori and beer.",
                            "Meiji Shrine: Visible from the observation deck and located a short distance away in Shibuya.",
                        },
                        BestTimeToVisit =
                            "9:30 AM (Winter): For your best shot at seeing Mt. Fuji.\n\n4:30 PM: To catch the \"Blue Hour\" when the sun sets and the city lights flicker on.\n\n7:30 PM (Varies): To catch the start of the evening projection mapping show on the building's exterior.",
                        crowdLevel =
                            "Moderate (6/10): Weekdays.\n\nHigh (9/10): Weekends and clear sunset periods.",
                        Accessibility =
                            "Rating: 10/10\n\nThe Terrain: Completely ADA-compliant. There are dedicated elevators for the observatories, wide corridors, and multi-purpose toilets. Wheelchair loans are also available at the information desk.",
                        IdealDuration =
                            "1 to 1.5 Hours\n\nThis covers the bag check, the elevator ride, a full loop of the deck, and a quick browse of the gift shop.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 5,
                    MinCost = 0,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = metroGovCatIds,
                    TagIds = metroGovTagIds,
                },
                // 12. Shinjuku Golden Gai
                new DestinationDto
                {
                    DestinationName = "Shinjuku Golden Gai",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/golden_gai.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Stepping into Shinjuku Golden Gai is like falling through a crack in Tokyo's neon-and-chrome facade. While the rest of Shinjuku reached for the sky, these six narrow alleys remained stuck in the 1950s. This \"shanty-style\" district is home to over 200 tiny, two-story bars, some so small they can only fit five customers at a time.\n\nHistorically a hub for the \"intellectual\" crowd—writers, directors, and actors—it has retained its ramshackle, mismatched charm despite the surrounding urban development. It is the antithesis of a polished hotel bar; it's cramped, dimly lit, and smells of history, woodsmoke, and whiskey.\n\nWhat to Do:\n\n- Bar Hopping: The quintessential Golden Gai experience. Don't settle for the first place you see. Try a theme bar like Deathmatch in Hell (horror/rock) or Ace's for a friendly, music-centric vibe.\n\n- Themed Discovery: Look for bars dedicated to specific niches like Jazz, R&B, Flamenco (Izakaya Nana), or even Horse Racing.\n\n- Meet the Locals: Because space is so tight, conversation is inevitable. This is one of the best places in Tokyo to talk to \"non-typical\" Japanese locals—artists, musicians, and long-time bartenders.\n\n- Visit the Theater: Catch a comedy show at the Shinjuku Golden Street Theatre located in a corner of the district.\n\n- Late Night Stroll: Even if you aren't drinking, walking through the alleys at 11:00 PM provides an atmosphere that feels like a movie set from an old Noir film.",
                        Directions =
                            "Best Station: JR Shinjuku Station (East Exit).\n\nKey Exit: East Exit.\n\nThe Direction Walk: From the East Exit, it is a 5 to 10-minute walk. Located between the Shinjuku City Office and Hanazono Shrine. Look for \"Mister Donut\" on Google Maps; the area is reached via a tree-lined street just behind and to the right of that storefront.\n\nNav Tip: Search for \"Shinjuku Golden Gai\" or \"Hanazono Shrine\" on Google Maps to find the entrance to the network of alleys.\n\nAddress for Taxi: 新宿区歌舞伎町1-1-6 (1-1-6 Kabukicho, Shinjuku).",
                        WhatToKnow =
                            "Currency Conversion: ($1 ≈ 150¥).\n\nCover Charges: Most bars charge a \"Table Charge\" or \"Cover\" ranging from 500¥ to 1,000¥. This is standard and pays for your seat in such a limited space.\n\nOpening Hours: Most bars don't even open their doors until 9:00 PM or 10:00 PM. The area is a ghost town during the day.\n\nFood: Very few bars serve actual meals. Eat dinner beforehand at Omoide Yokocho (Piss Alley) nearby.\n\nPro-Tip: Look for the \"No Cover Charge\" signs if you're on a budget, but remember that bars with cover charges often provide a small snack (otoshi) like nuts or a small bowl of pasta salad.",
                        ThingsToBeWaryOf =
                            "Strict Etiquette: Respect the \"Regulars Only\" signs; if a bar looks closed or unwelcoming, it likely prefers local regulars.\n\nSocial Battery: If you are claustrophobic, Golden Gai might be a challenge. You will be touching elbows with strangers.\n\nSunday Slump: Many bars close on Sundays. If you visit then, you'll miss the vibrant energy.\n\nThe Bill: One drink + cover charge can easily hit 1,500¥–2,000¥ (~$10–13 USD). If you hop to five bars, your night becomes expensive quickly.\n\nSteep Stairs: Many second-story bars are reached by very steep, narrow staircases that can be difficult to navigate.\n\nSpace Constraints: Bars are extremely small; be prepared to get \"cozy\" with strangers.",
                        LocalPerspective =
                            "The \"Regulars Only\" Rule: If a door is closed and there is no English sign or price list, it might be a \"members only\" bar. Don't take it personally—these spots are living rooms for long-time regulars.\n\nThe Yakuza Guard: In the 1980s, locals and supporters famously took turns guarding the area to prevent developers (and arsonists) from destroying the district. There is a deep pride in its survival.\n\nPhotography Protocol: The alleys are private roads. Street photography and filming are officially prohibited without permission from the business association. Be discreet or ask before snapping photos of bar interiors.",
                        HiddenCost =
                            "Cover Fees (Otoshi): 500 to 1,000 yen (~$3.33 – $6.67 USD) per bar.\n\nThe \"Tourist Tax\": While not an official tax, bars that are very \"tourist-friendly\" (English signs out front) sometimes have slightly higher drink prices than the hidden, local-only spots.\n\nPremium Drink Prices: Drinks can be expensive, sometimes reaching 1,500 yen for a single cocktail.\n\nTaxi Surcharge: Late-night returns to other parts of Tokyo will require a taxi, which often adds a late-night surcharge.",
                        NearbyComplements = new List<string>
                        {
                            "Hanazono Shrine: A beautiful, calm Shinto shrine located right next to the chaos of Golden Gai.",
                            "Omoide Yokocho (Piss Alley): Great for yakitori and a \"pre-game\" meal before you head to Golden Gai.",
                            "Kabukicho: The broader red-light and entertainment district surrounding the area.",
                            "Thermae-Yu: A massive 6-story 24-hour onsen/spa just 5 minutes away. Perfect for soaking off the \"bar smell\" before heading home.",
                        },
                        BestTimeToVisit =
                            "Weeknights (Mon-Thu): For a more authentic experience with fewer tourists than the weekend rush.\n\n10:00 PM – 1:00 AM: This is when the area is at its most electric.\n\nWeekdays: Slightly fewer tourists, making it easier to snag a stool in the most popular tiny bars.",
                        crowdLevel =
                            "Extreme (9/10): Especially on Friday and Saturday nights. You may have to check three or four bars before finding a single open seat.",
                        Accessibility =
                            "Rating: 2/10\n\nThe Terrain: The alleys are extremely narrow and uneven. Most bars are accessible only by very steep, ladder-like stairs. It is unfortunately not wheelchair-friendly.",
                        IdealDuration =
                            "2 to 4 Hours\n\nThis gives you enough time to experience 2 or 3 different bars and have a few meaningful conversations.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 45,
                    MinCost = 15,
                    SafetyLevel = 9,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = goldenGaiCatIds,
                    TagIds = goldenGaiTagIds,
                },
                // 13. Kabukicho
                new DestinationDto
                {
                    DestinationName = "Kabukicho",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/kabukicho.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Kabukicho is Tokyo's legendary \"Sleepless Town\" (Shinjuku no Fuyajo). Located just northeast of Shinjuku Station, it is the largest and most infamous entertainment district in Japan. While it historically carries a gritty reputation as a red-light district, recent massive redevelopments—like the Tokyu Kabukicho Tower—have transformed it into a hybrid of world-class entertainment and neon-drenched chaos. It's a sensory overload of thousands of bars, nightclubs, karaoke parlors, and \"love hotels,\" all coexisting under a sea of glowing signs.\n\nWhat to Do:\n\n- The Godzilla Encounter: Visit the 8th-floor terrace of the Hotel Gracery to see the life-size Godzilla head. It roars and breathes smoke every hour on the hour (12 PM–8 PM).\n\n- Arcade Hopping: Head to namco TOKYO inside the Kabukicho Tower for a \"Neo-Tokyo\" gaming experience featuring high-tech claw machines and AI DJ lounges.\n\n- Immersive Quests: Challenge the Sword Art Online: Anomaly Quest, an interactive escape-room style dungeon inside the main tower.\n\n- Retro Dining: Eat your way through Shinjuku Kabuki Hall, an indoor food hall themed after traditional Japanese festivals and regional soul food.\n\n- Cultural Contrast: Take a 2-minute walk to the serene Hanazono Shrine to see where the district's performers pray for success.\n\n- Themed Bar Hopping: Explore Golden Gai's diverse bars, featuring themes like punk rock, jazz, or exploitation films.\n\n- Cultural Experience: Visit the SAMURAI & NINJA MUSEUM to try on armor or the Ninja Trick House for interactive puzzles.",
                        Directions =
                            "Best Station: JR Shinjuku Station or Seibu-Shinjuku Station.\n\nKey Exit: From JR Shinjuku Station: You must find the East Exit (or the Studio Alta exit). If you find yourself at the \"South\" or \"West\" exits, it is a long, confusing walk around the tracks. From Seibu-Shinjuku Station: Take the Main Exit; you are practically already in the district.\n\nThe Direction Walk: Once you come out of the JR Shinjuku East Exit, you will see a large plaza with a giant video screen (Studio Alta). Walk toward the large multi-lane road (Yasukuni-dori) directly ahead. Cross the street toward the Don Quijote (it has a massive yellow sign). The entrance to Kabukicho is the street marked by a famous red neon archway (Kabukicho Ichibangai) right next to it.\n\nLandmark: Look up! You should see the Gracery Hotel with a life-sized Godzilla head peeking over the roof. Walk toward Godzilla, and you are in the heart of the district.\n\nAddress for Taxi: 東京都新宿区歌舞伎町1丁目",
                        WhatToKnow =
                            "The Touts: You will be approached by men in suits or tracksuits offering \"cheap drinks\" or \"girls.\" Ignore them. They are professional recruiters for bars that often engage in overcharging scams.\n\nThe \"Last Train\" Dash: Around 12:00 AM, you'll see a literal stampede of people heading back to Shinjuku Station to catch the last train. If you miss it, be prepared to stay until 5:00 AM or pay for an expensive taxi.\n\nCash Flow: While the newer towers take cards/Apple Pay, most small izakayas and bars in the area are still cash-only.",
                        ThingsToBeWaryOf =
                            "Bottakuri (Rip-offs): Never enter a bar recommended by a stranger on the street. Some bars charge hidden \"sitting fees\" or \"service taxes\" that can turn a $20 night into a $500 bill.\n\nDrink Spiking: While rare, there have been 2026 reports of drink-spiking in \"hidden\" bars. Stick to reputable, well-reviewed spots.\n\nPhotography: Avoid taking direct photos of host/hostess club staff or people working in the \"deep\" areas; they value their privacy and can be confrontational.",
                        LocalPerspective =
                            "Day vs. Night: During the day, Kabukicho is actually quite family-friendly and great for photography. The \"vibe\" shifts dramatically after 9 PM.\n\nThe \"Blue Zone\" vs. \"Red Zone\": Locals generally stick to the Blue Zone (the main well-lit central roads near the cinema) for dining. The Red Zone (northern backstreets) is where the \"adult\" establishments are concentrated and require more street smarts.",
                        HiddenCost =
                            "Table Charges: Expect to pay an otoshi (appetizer/cover charge) of 500¥ to 1,000¥ per person at most bars. This is mandatory and often comes with a small, unsolicited snack.\n\nMidnight Surcharge: Taxis in Tokyo increase their rates by 20% between 10 PM and 5 AM.",
                        NearbyComplements = new List<string>
                        {
                            "Golden Gai: A collection of 200+ tiny themed bars just a 3-minute walk east.",
                            "Omoide Yokocho: Also known as \"Piss Alley,\" it's the go-to spot for yakitori and beer before heading into the neon lights of Kabukicho.",
                            "Thermae-Yu: A massive 24-hour natural hot spring spa perfect for recovering after a long night out.",
                        },
                        BestTimeToVisit =
                            "8:00 PM – 11:00 PM: The \"Golden Hour\" where the neon is brightest but the crowds are still mostly tourists and regular diners.",
                        crowdLevel =
                            "9/10 (High): Especially on Friday and Saturday nights. The main thoroughfares can feel like a packed mosh pit.",
                        Accessibility =
                            "Rating: 7/10\n\nThe main streets are flat and paved. Most major attractions (Kabukicho Tower, Toho Cinemas) are fully ADA-compliant. However, many small basement bars are only accessible via narrow, steep stairs.",
                        IdealDuration =
                            "3 to 5 Hours (Enough time for dinner, some arcade games, and Godzilla-spotting).",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                    },
                    MaxCost = 100,
                    MinCost = 5,
                    SafetyLevel = 7,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = kabukichoCatIds,
                    TagIds = kabukichoTagIds,
                },
                // 14. Tsukiji Outer Market
                new DestinationDto
                {
                    DestinationName = "Tsukiji Outer Market",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/tsukiji.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Tsukiji Outer Market is Tokyo's \"Food Town,\" a resilient district that remained vibrant even after the inner wholesale market (famous for the tuna auctions) moved to Toyosu in 2018. Built on \"constructed land\" reclaimed in the 1600s, it serves as a bridge between professional chefs and food-loving travelers.\n\nThe market is a dense maze of over 400 shops selling everything from high-end Uni (sea urchin) and Bluefin Tuna to professional-grade Japanese knives and handmade ceramics. It is a sensory journey where the smell of grilled scallops meets the rhythmic sound of knives on cutting boards, offering an authentic taste of Tokyo's \"Shitamachi\" (old town) spirit.\n\nWhat to Do:\n\nSushi Breakfast: Dine at local restaurants that serve seafood delivered directly from the Toyosu Market.\n\nProfessional Shopping: Browse specialty shops for dried bonito flakes, seaweed, high-quality knives, and authentic Japanese tableware.\n\nStreet Food Crawl: Sample ready-to-eat items like tamagoyaki (omelets), sashimi rice bowls, and fresh oysters.\n\nCultural Pilgrimage: Visit the Namiyoke Inari Shrine at the corner of the market, which has served as a \"guardian against waves\" for the district since the 1600s.\n\nExplore Tsukiji Uogashi: Visit this newer facility to find fresh seafood and produce in a more organized wholesale setting.",
                        Directions =
                            "Best Stations: Tsukiji Shijo Station (Oedo Subway Line) or Tsukiji Station (Hibiya Subway Line).\n\nKey Exit: From Tsukiji Shijo Station: A short walk from the station exit. From Tsukiji Station: Reach the market within minutes of exiting.\n\nThe Direction Walk: From Tokyo Station, take the Marunouchi Line to Ginza and transfer to the Hibiya Line for Tsukiji Station. From Shinjuku Station, take the Oedo Line directly to Tsukiji Shijo Station (approx. 20 minutes). From Shimbashi Station, the market is a roughly 20-minute walk.\n\nAddress for Taxi: 東京都中央区築地4-16-2 (4-16-2 Tsukiji, Chuo-ku).",
                        WhatToKnow =
                            "Operating Hours: Most restaurants open as early as 5:00 AM and close by noon or early afternoon.\n\nCrowd Warning: The market can become extremely crowded; early morning visits are highly recommended to avoid the heaviest tourist traffic.\n\nProfessional Etiquette: While retail customers are welcome, remember that many shops still serve professional chefs; be mindful of business operations in narrow passageways.\n\nToyosu vs. Tsukiji: If you want to see the Tuna Auction, you must go to Toyosu Market (a few miles away). If you want to eat and shop, stay at Tsukiji Outer Market.\n\nCash Flow: While large restaurants take cards, the best street food stalls are strictly cash-only.\n\nTax-Free: Many kitchenware and dried goods shops offer tax-free shopping for tourists (bring your passport!).\n\nPro-Tip: If the line for a famous sushi shop is 2 hours long, look for \"Tsukiji Uogashi\" (the multi-story building). The food court on the top floor often has high-quality seafood with much shorter wait times!",
                        ThingsToBeWaryOf =
                            "Overtourism Measures: Be aware that some popular Tokyo landmarks have begun implementing temporary access restrictions during holidays to manage crowds.\n\nPricing: Some specialty items, such as rare tuna cuts or aged whiskies, can be very expensive.\n\nMaze-like Streets: The intricate layout of the market can be confusing; keep a digital or PDF guide map handy.",
                        LocalPerspective =
                            "\"Constructed Land\": The name \"Tsukiji\" literally means \"constructed land,\" as the area was reclaimed from Tokyo Bay following a great fire in 1657.\n\nProfessional Roots: The market began in 1935 after the Great Kanto Earthquake destroyed the previous Nihonbashi Fish Market.\n\nHidden Gems: Locals and connoisseurs seek out rare delicacies like nodoguro (blackthroat seaperch) or high-end sake bars and whiskey lounges tucked away in the alleys.\n\nAfternoon Ghost Town: Most shops begin closing by 1:00 PM or 2:00 PM. By 3:00 PM, the market is almost entirely shut down. This is an early morning destination only.\n\nThe \"Don't Walk and Eat\" Rule: Unlike many Western markets, \"eating while walking\" (tabearuki) is traditionally frowned upon here to keep the narrow lanes clean. Most stalls provide a small standing space to finish your snack before moving on.",
                        HiddenCost =
                            "Premium Ingredients: High-end merchandise and rare fish varieties (like otoro) command professional prices.\n\nShipping & Delivery: Services for domestic delivery or tax-refund procedures may involve additional fees.",
                        NearbyComplements = new List<string>
                        {
                            "Ginza: A high-end shopping and dining district only a few minutes away by train or a 15-minute walk.",
                            "Hamarikyu Gardens: A beautiful landscape garden located near the market, perfect for a post-breakfast stroll.",
                            "Kabuki-za Theatre: Located nearby in Higashi-Ginza for a traditional Japanese performance.",
                        },
                        BestTimeToVisit =
                            "8:00 AM – 10:00 AM: The sweet spot where all shops are open, but the massive midday tourist lunch crowds haven't fully peaked yet.",
                        crowdLevel =
                            "10/10 (High): Expect tight quarters, queues for popular sushi spots, and a \"shoulder-to-shoulder\" experience in the main alleys.",
                        Accessibility =
                            "Rating: 6/10\n\nWhile the main areas are accessible, many individual shops and older restaurants are located in narrow, cramped alleys that may be difficult for wheelchairs or strollers to navigate.",
                        IdealDuration =
                            "2 to 3 Hours\n\nSufficient for a sushi meal and a leisurely walk through the primary wholesale and retail blocks.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 60,
                    MinCost = 5,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = tsukijiCatIds,
                    TagIds = tsukijiTagIds,
                },
                // 15. teamLab Borderless: Azabudai Hills
                new DestinationDto
                {
                    DestinationName = "teamLab Borderless: Azabudai Hills",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/teamlab.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Reopened in early 2024 at the prestigious Azabudai Hills complex, teamLab Borderless is the \"museum without a map.\" Unlike traditional galleries, the art here literally walks out of the rooms—dragons fly down hallways, and flowers bloom where you stand. It is a 10,000-square-meter maze of over 75 interconnected digital works that evolve in real-time based on your presence. In 2026, it remains one of the world's most visited art museums, featuring fan favorites like the \"Forest of Resonating Lamps\" alongside newer, world-exclusive installations like \"Bubble Universe.\"\n\nWhat to Do:\n\nThe Wander Rule: There is no map. If you see a dark curtain or a narrow hallway, go through it. Some of the best rooms, like the Light Vortex or the Crystal World, are easily missed if you follow the crowd.\n\nEN TEA HOUSE: This is a mandatory \"extra.\" You buy a cup of matcha, and a digital flower \"blooms\" inside your tea. When you move the cup, the petals scatter. It's one of the most serene, high-tech tea ceremonies on Earth.\n\nSketch Ocean: Color in a sea creature on paper, scan it, and watch it swim onto the walls of the giant digital aquarium. In 2026, your creations can even \"travel\" to other teamLab exhibits worldwide.\n\nInteract with the Walls: Almost everything is touch-sensitive. Touch the \"Universe of Water Particles\" (the giant waterfall) and the water will flow around your body like a real rock.\n\nBubble Universe: A newer room featuring hundreds of glass spheres that react to your movement, creating a cascading light show that feels like being inside a galaxy.",
                        Directions =
                            "Best Station: Kamiyacho Station (Tokyo Metro Hibiya Line).\n\nKey Exit: Exit 5.\n\nThe Direction Walk: The museum is a 2-minute walk from Kamiyacho Station. It is located within the Azabudai Hills development.\n\nFrom Major Hubs: From Tokyo Station: Approximately 12 minutes via the Marunouchi Line (change at Ginza to the Hibiya Line). From Shinjuku Station: Approximately 24 minutes via the Marunouchi Line (change at Kasumigaseki to the Hibiya Line).\n\nAddress for Taxi: 東京都港区麻布台1-2-4",
                        WhatToKnow =
                            "Variable Pricing: Ticket prices change depending on the day. Weekends and holidays (like the upcoming Golden Week in early May) are significantly more expensive and sell out weeks in advance.\n\nBooking: It is strongly advised to book tickets online in advance; on-site tickets incur an additional 200 yen fee and may be sold out.\n\nFootwear: Unlike teamLab Planets, you keep your shoes on here. Wear comfortable sneakers; you will be walking and standing for 3+ hours.\n\nMirrored Floors: Many rooms have mirrored floors. If you are wearing a short skirt, it is highly recommended to wear leggings or shorts underneath (though they do offer \"wrap-around skirts\" for borrowing if needed).\n\nPro-Tip: If you see a crowd waiting for a specific room, keep walking! The artworks \"migrate\" through the halls. You might see the same dragons or characters in a completely empty hallway five minutes later.",
                        ThingsToBeWaryOf =
                            "Battery Life: You will take more photos and videos here than anywhere else in Japan. Bring a portable power bank. Most visitors check their bags in the lockers before entering, but make sure to keep your charger with you!\n\nStaff Policies: Be aware that staff may enforce strict policies regarding movement and interaction to protect the artwork.\n\nSensory Overload: The \"Light Vortex\" room features high-speed flashing lights and spinning beams. It can be intense for those prone to motion sickness or photosensitivity.\n\nCrowd Flow: Touts aren't an issue here, but \"Influencer Traffic Jams\" are. Be patient in the Lamp Room, as there is often a timed entry (usually 1-2 minutes per group).",
                        LocalPerspective =
                            "The \"Canvas\" Dress Code: Locals know to wear white or light-colored clothing. The digital art is projected onto you, making your body a literal part of the artwork.\n\nSeasonal Shifts: The art reflects the real-world seasons of Tokyo. If you visit in April 2026, you will see digital cherry blossoms (Sakura) and budding rice plants in the \"Memory of Topography\" room.\n\nAvoid the Lunch Rush: The Azabudai Hills complex is a business hub. Visiting at 12:00 PM means competing with thousands of office workers for nearby lunch spots.\n\nBorderless vs. Planets: While \"teamLab Planets\" involves wading through water and removing shoes, \"Borderless\" allows you to explore with shoes on and emphasizes the intermingling of artworks across rooms.",
                        HiddenCost =
                            "EN TEA HOUSE: Not included in your ticket. Expect to pay about 600¥ – 1,100¥ per person for the tea experience.\n\nLockers: Available on-site (usually 100¥, often refundable). Use them! Carrying a heavy backpack through the mirrors and narrow passages will ruin your experience.",
                        NearbyComplements = new List<string>
                        {
                            "Tokyo Tower: A 10-minute walk away. The view of the tower from the Azabudai Hills plaza is one of the best in the city.",
                            "Mori Art Museum (Roppongi Hills): Just one station away if you haven't had enough art for the day.",
                            "Janu Tokyo: The luxury hotel inside Azabudai Hills has incredible (but pricey) afternoon tea if you want to stay in the upscale vibe.",
                        },
                        BestTimeToVisit =
                            "9:00 AM (Opening) or after 6:00 PM: The museum is often open until 9:00 PM. Visiting late allows you to avoid the school groups and tourist rushes.",
                        crowdLevel =
                            "8/10 (High): It's a global bucket-list item. Even with timed entry, the popular rooms will have short queues.",
                        Accessibility =
                            "Rating: 8/10\n\nMostly wheelchair and stroller accessible. There are specific \"barrier-free\" routes provided by staff, though a few \"Athletics Forest\" areas with uneven terrain may be restricted.",
                        IdealDuration =
                            "3 to 4 Hours\n\nBecause there is no set path, you can easily get lost (intentionally) for half a day.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.JanToMarch,
                        TravelPeriod.AprToJun,
                        TravelPeriod.JulToSep,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 30,
                    MinCost = 10,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = teamlabCatIds,
                    TagIds = teamlabTagIds,
                },
                // 16. Tokyo Disney
                new DestinationDto
                {
                    DestinationName = "Tokyo Disney",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/disney_tokyo.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Tokyo Disney Resort (TDR) is a premier vacation destination in Urayasu, Chiba, consisting of Tokyo Disneyland (TDL) and Tokyo DisneySea (TDS). In 2026, the resort is celebrating the 25th Anniversary of DisneySea with the \"Sparkling Jubilee.\" Owned and operated by the Oriental Land Co., the resort is famous for its unmatched maintenance, seasonal food, and the massive 2024 expansion, Fantasy Springs. It remains a \"bucket list\" destination for Disney fans worldwide due to its unique nautical-themed park and high-tech attractions.\n\nWhat to Do:\n\n- Fantasy Springs (TDS): Explore the lands of Frozen, Tangled, and Peter Pan. Most rides are now on Standby, but wait times are significant (100–180+ mins).\n\n- \"Sparkling Jubilee\" (TDS): View the 25th-anniversary harbor show and the world-dance stage show \"Dance the Globe!\" featuring Mirabel and Hiro.\n\n- Enchanted Tale of Beauty and the Beast (TDL): Ride the world-renowned trackless dark ride in Fantasyland.\n\n- Seasonal Snacking: Try the 2026 Jubilee Blue Macarons or classic Alien Mochi.\n\n- Ikspiari Shopping: Visit the \"Downtown Disney\" of Japan for dining and resort-exclusive merch.",
                        Directions =
                            "Best Station: JR Maihama Station (JR Keiyo Line from Tokyo Station).\n\nKey Access: From Tokyo Station: Reach Maihama in approximately 15 minutes via the JR Keiyō Line.\n\nDisney Resort Line: A monorail system that connects Maihama Station to the theme parks and resort hotels.\n\nShuttle Services: The Tokyo Disney Celebration Hotel is connected to the resort via a free 15-minute shuttle.\n\nAddress for Taxi: English: 1-1 Maihama, Urayasu, Chiba. Japanese: 千葉県浦安市舞浜1-1 (東京ディズニーリゾート). Note: Specify \"Disneyland\" or \"DisneySea\" drop-off to save walking time.",
                        WhatToKnow =
                            "Ticket Tiers (USD @ 160¥): Value: ¥7,900 (~$49); Regular: ¥9,400 (~$59); Peak: ¥10,900 (~$68).\n\nDisney Premier Access (DPA): Paid line-skipping (approx. $9–$15 per ride). Highly recommended for Anna and Elsa's Frozen Journey.\n\nMobile Order: Use the TDR App to order food early in the morning to avoid 40+ minute lunch lines.\n\nBooking Tickets: Buy tickets online 30–60 days in advance, as they can sell out for popular dates.\n\nLanguage Barrier: While most dialogue in attractions is Japanese, signs and maps are in English, and Cast Members are exceptionally helpful with limited English.\n\nWait Times: Tokyo DisneySea often experiences higher wait times than Disneyland, with headliners frequently exceeding 100 to 180 minutes.\n\nPro-Tip: The \"Value\" price of $49 makes this one of the best entertainment deals in the world right now. If you can avoid the Golden Week rush, you'll be getting a premier Disney experience for a fraction of the usual global cost.",
                        ThingsToBeWaryOf =
                            "Golden Week (Current): You are in the middle of Japan's busiest holiday week (April 29 – May 5). Expect max-capacity crowds and ¥10,900 ticket pricing.\n\nConstruction Zone: Tomorrowland (TDL) is currently a heavy construction site as the new Space Mountain is built for 2027.\n\nLast Train: The Keiyo Line to Tokyo becomes a \"mosh pit\" after the fireworks. Consider leaving 20 minutes early or staying for a late dinner at Ikspiari.",
                        LocalPerspective =
                            "The 160¥ Exchange Rate: With the Yen currently at 160¥ to $1, your dollar is stronger than it has been in decades. This makes luxury dining and high-end merchandise significantly cheaper for USD holders.\n\nSitting for Shows: Locals sit for parades and harbor shows. Bring a small plastic sheet to claim your space.\n\nDisproportionate Local Attendance: TDR is heavily driven by repeat local visitors, which leads to high demand for new seasonal events and specific new lands like Fantasy Springs.\n\nRule Adherence: In Japan, rules and meal menu items are typically inflexible; requests for substitutions are often met with confusion or resistance.\n\nRope Drop: If the park \"officially\" opens at 9:00 AM, expect them to open the gates at 8:15 AM. Arrive by 7:15 AM to be at the front of the pack.",
                        HiddenCost =
                            "Table Charges: Restaurants in Ikspiari may have a small cover charge (otoshi) in the evenings.\n\nLine-Skipping Fees: To maximize a single day, many visitors feel compelled to purchase multiple Disney Premier Access passes.\n\nDPA Stacking: If you buy DPA for three major rides, you're adding roughly $35–$40 to your daily per-person cost.",
                        NearbyComplements = new List<string>
                        {
                            "Kasai Rinkai Park: One stop away, featuring a giant Ferris wheel and aquarium.",
                            "Spa & Hotel Resort: Many \"Official\" hotels (Hilton, Sheraton) offer massive breakfast buffets and public baths.",
                            "Chiba City: Accessible for those looking to explore beyond the immediate resort area.",
                        },
                        BestTimeToVisit =
                            "Tuesdays – Thursdays: To avoid the local weekend rush.\n\nMid-May: Immediately after Golden Week ends (May 7th onwards) is the \"sweet spot\" for 2026.",
                        crowdLevel =
                            "10/10 (Current - Golden Week/Anniversary)\n\n7/10 (Standard Weekday)",
                        Accessibility =
                            "Rating: 9/10\n\nExcellent flat surfaces and wide paths. The Monorail is fully accessible with elevators at every station.",
                        IdealDuration =
                            "3 to 4 Days (1 day for TDL, 2 days for TDS due to Fantasy Springs, 1 day for rest/shopping).",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 68,
                    MinCost = 49,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = disneyCatIds,
                    TagIds = disneyTagIds,
                },
                // 17. Akihabara
                new DestinationDto
                {
                    DestinationName = "Akihabara",
                    DestinationImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/akihabara.jpg",
                    Description = new DescriptionJsonDto
                    {
                        Overview =
                            "Commonly known as Akiba, Akihabara is Tokyo's vibrant \"Electric Town.\" Historically a post-WWII black market for radio parts, it evolved into a global tech showcase before transforming into the spiritual heart of Otaku culture. Today, it is a dense neon jungle where massive electronics retailers like Yodobashi Camera stand alongside multi-story shrines dedicated to anime, manga, and retro gaming. It is the birthplace of the Maid Café phenomenon and a primary stage for Japan's idol culture.\n\nWhat to Do:\n\nElectronics Hunting: Visit Yodobashi Camera for the latest tech or dive into the tiny stalls of Akihabara Radio Centre for niche circuit parts.\n\nOtaku Pilgrimage: Browse the 10 floors of Radio Kaikan for figurines and collectibles, or explore Mandarake for rare, vintage manga.\n\nRetro Gaming: Visit Super Potato to find every console ever made and play classic arcade games on the top floor.\n\nMaid Cafés: Experience a unique subculture at @Home Cafe or Maidreamin, where servers treat you as \"master\" of the house.\n\nGachapon Craze: Spend your loose change at Akihabara Gachapon Hall, which houses hundreds of capsule toy machines.",
                        Directions =
                            "Best Station: Akihabara Station (JR Yamanote, Keihin-Tohoku, Sobu Lines; Hibiya Subway; Tsukuba Express).\n\nSecondary Station: Suehirocho Station (Ginza Line) for the northern end of the district.\n\nFrom Tokyo Station: 3 minutes via JR Yamanote Line (¥160).\n\nAddress for Taxi: English: Akihabara Station, Sotokanda, Chiyoda City, Tokyo. Japanese: 千代田区外神田 (秋葉原駅)",
                        WhatToKnow =
                            "Tax-Free Shopping: Most major stores (Don Quijote, Laox, Sofmap) offer 10% tax-free shopping for tourists. Keep your passport on you to claim the discount at the register.\n\nVoltage Warning: Japanese electronics run on 100V. While many modern devices are dual-voltage, always check the label before buying high-power appliances for use back home.\n\nEtiquette: Always ask permission before photographing cosplayers or maid café staff on the street.\n\nPro-Tip: Looking for the cheapest way to eat like a local? Head to the \"vending machine corner\" for a can of hot Oden—an Akihabara staple for busy otaku on the go!",
                        ThingsToBeWaryOf =
                            "Maid Café \"Touts\": Avoid aggressive promoters on the street who don't clearly list their \"cover charge\" (nyuteryo). Stick to well-known chains to avoid hidden fees.\n\nThe \"Tourist Trap\" Markup: Shops on the main road are often pricier. For better prices on used goods, walk 2–3 blocks into the side alleys.\n\nSundays: While the pedestrian street is great, it is also the most crowded time for shops and restaurants.",
                        LocalPerspective =
                            "The Pedestrian Paradise: On Sunday afternoons, the main thoroughfare (Chuo Dori) closes to cars. It is the best time for photography and soaking in the atmosphere.\n\nHidden Gems: The best deals on used figures are often in \"Rental Showcases\" (small glass boxes rented by individuals) in the side streets.\n\nThe 160¥ Advantage: With the Yen at 160 to $1, retro games and high-end figurines are significantly more affordable for USD holders in 2026.\n\nEvolving Identity: The district shifted from household appliances to home computers in the 1980s, which attracted the \"computer nerds\" who eventually formed the modern otaku base.\n\nSunday Pedestrians: On Sundays, the main thoroughfare, Chuo Dori, is closed to car traffic from 13:00 to 18:00 (17:00 in winter), allowing for easy strolling.",
                        HiddenCost =
                            "Maid Café Fees: Expect a cover charge (usually ¥600–¥1,000) in addition to the mandatory one-drink minimum.\n\nArcade Addiction: Modern \"Crane Games\" (UFO catchers) are designed to be difficult; it's easy to drop $20–$30 chasing a single prize.\n\nTheater Tickets: Shows at the AKB48 Theatre typically cost between 2,400 and 3,400 yen.",
                        NearbyComplements = new List<string>
                        {
                            "Kanda Myojin Shrine: A beautiful, tech-friendly shrine nearby where you can buy charms to protect your electronic devices.",
                            "mAAch ecute Kanda Manseibashi: A stylish shopping mall built into an old brick railway viaduct over the river.",
                            "Ochanomizu: Nearby neighborhood known for its high concentration of musical instrument shops and bookstores.",
                        },
                        BestTimeToVisit =
                            "Sundays 1:00 PM – 5:00 PM: For the car-free experience.\n\nWeeknight Evenings: To see the neon lights at their most impressive without the massive weekend crowds.",
                        crowdLevel =
                            "Crowd Level: 9/10 (High). Particularly intense on weekends and during the Sunday pedestrian hours.\n\n6/10 (Weekday mornings)",
                        Accessibility =
                            "Rating: 8/10\n\nMost modern buildings have elevators, but the famous \"Radio Centre\" stalls and older hobby shops have very narrow aisles that are difficult for wheelchairs.",
                        IdealDuration =
                            "4 to 6 Hours for a general visit; a full day if you are a serious collector or hobbyist.",
                    },
                    BestPeriodToVisit = new List<TravelPeriod>
                    {
                        TravelPeriod.AprToJun,
                        TravelPeriod.OctToDec,
                    },
                    MaxCost = 100,
                    MinCost = 5,
                    SafetyLevel = 10,
                    TimeZone = "Japan Standard Time",
                    CountryId = japanCountryId,
                    CityIds = tokyoCityIds,
                    LanguageIds = japanLangIds,
                    CurrencyIds = japanCurrencyId,
                    CategoryIds = akihabaraCatIds,
                    TagIds = akihabaraTagIds,
                },
            ];

            var newDestinationList = tokyoDestinationList
                .Select(destination =>
                {
                    if (
                        !context.Destinations.Any(d =>
                            d.DestinationName == destination.DestinationName
                            && d.CountryId == destination.CountryId
                        )
                    )
                    {
                        return destination.FromDtoToDestination();
                    }
                    return null;
                })
                .Where(d => d != null)
                .ToList();

            await context.Destinations.AddRangeAsync(newDestinationList);
            await context.SaveChangesAsync();
        }

        public static async Task SeedCityAsync(CustomDbContext context)
        {
            var cityData = new Dictionary<string, string>
            {
                { "Los Angeles", "United States" },
                { "Miami", "United States" },
                { "Dallas", "United States" },
                { "Vancouver", "Canada" },
                { "Toronto", "Canada" },
                { "Victoria", "Canada" },
                { "Monterrey", "Mexico" },
                { "Mexico City", "Mexico" },
                { "Guadalajara", "Mexico" },
                { "Tokyo", "Japan" },
                { "Kyoto", "Japan" },
                { "Osaka", "Japan" },
                { "Rome", "Italy" },
                { "Venice", "Italy" },
                { "Naples", "Italy" },
                { "Paris", "France" },
                { "Lyon", "France" },
                { "Nice", "France" },
                { "London", "United Kingdom" },
                { "Edinburgh", "United Kingdom" },
                { "York", "United Kingdom" },
                { "Santorini", "Greece" },
                { "Athens", "Greece" },
                { "Chania", "Greece" },
                { "Bangkok", "Thailand" },
                { "Phuket", "Thailand" },
                { "Chiang Mai", "Thailand" },
            };

            List<City> newCities = new List<City>();

            var availableCities = await context.Cities.ToListAsync();
            if (!availableCities.Any())
            {
                var countries = await context.Countries.ToDictionaryAsync(
                    c => c.CountryName,
                    c => c.CountryId
                );

                newCities = cityData
                    .Select(cd =>
                    {
                        return new City
                        {
                            CityId = Guid.NewGuid(),
                            CityName = cd.Key,
                            CountryId = countries[cd.Value],
                        };
                    })
                    .ToList();

                await context.AddRangeAsync(newCities);
                await context.SaveChangesAsync();
            }
        }
    }
}
