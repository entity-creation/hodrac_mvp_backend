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
            var sanBlasIds = await context
                .Destinations.Where(d =>
                    d.DestinationCities.Any(dc => dc.City.CityName == "Guna Yala")
                )
                .Select(dest => dest.DestinationId)
                .ToListAsync();
            Console.WriteLine($"San blas ${sanBlasIds}");
            var panamaIds = await context
                .Destinations.Where(d =>
                    d.DestinationCities.Any(dc => dc.City.CityName == "Panama City")
                )
                .Select(dest => dest.DestinationId)
                .ToListAsync();
            var bocasIds = await context
                .Destinations.Where(d =>
                    d.DestinationCities.Any(dc =>
                        dc.City.CityName == "Caribbean coast of Panama (Northwest)"
                    )
                )
                .Select(dest => dest.DestinationId)
                .ToListAsync();
            var guidList = new Dictionary<String, List<Guid>>
            {
                { "San Blas", sanBlasIds },
                { "Panama City", panamaIds },
                { "Bocas", bocasIds },
            };
            List<QueryWishlistDto> wishlists =
            [
                new QueryWishlistDto
                {
                    WishlistId = Guid.NewGuid(),
                    WishlistHeroImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/san-blas-hero.jpg",
                    WishlistDescription =
                        "Turquoise water, hammocks over the sea, and fresh lobster on the sand — no wifi, no worries.",
                    WishlistName = "San Blas Relax Trip",
                    ShortStory =
                        "Spend three days island-hopping through the San Blas archipelago, sleeping in rustic cabins over crystal-clear water, snorkeling shipwrecks, and eating lobster on the sand.",
                    TotalDays = 3,
                    PeopleType = "Best for couples / slow travelers",
                    ItineraryDays =
                    [
                        new ClientItineraryDayDto
                        {
                            DayNumber = 1,
                            DayTitle = "Arrival & First Island Escape",
                        },
                        new ClientItineraryDayDto
                        {
                            DayNumber = 2,
                            DayTitle = "Sandbars & Lobster Lunch",
                        },
                        new ClientItineraryDayDto
                        {
                            DayNumber = 3,
                            DayTitle = "Slow Morning & Return",
                        },
                    ],
                    ItineraryItems =
                    [
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Drive from Panama City to Cartí",
                            ItemOrderIndex = 0,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Boat transfer into San Blas",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Arrive at Isla Perro",
                            ItemOrderIndex = 2,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Snorkel shipwreck reef",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Lunch on the island",
                            ItemOrderIndex = 4,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Overnight on Isla Diablo",
                            ItemOrderIndex = 5,
                            ItemType = "Location",
                            TimeOfDay = "Evening",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Morning at The Pool sandbar",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Boat to Banedup Island",
                            ItemOrderIndex = 1,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Lunch at Ibin's Beach Restaurant",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Afternoon snorkeling near Dutch Cays",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Overnight overwater cabin",
                            ItemOrderIndex = 4,
                            ItemType = "Location",
                            TimeOfDay = "Evening",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Breakfast by the sea",
                            ItemOrderIndex = 0,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Visit Kuna village",
                            ItemOrderIndex = 1,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Return boat to Cartí",
                            ItemOrderIndex = 2,
                            ItemType = "Location",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Drive back to Panama City",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                    ],
                },
                new QueryWishlistDto
                {
                    WishlistId = Guid.NewGuid(),
                    WishlistHeroImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/panamacity.jpeg",
                    WishlistDescription =
                        "History, skyline views, the Panama Canal, and fresh seafood — the perfect introduction.",
                    WishlistName = "Panama City Highlights (Pacific Side)",
                    ShortStory =
                        "Spend two days exploring Panama City’s historic streets, walking along the waterfront skyline, and witnessing one of the greatest engineering feats in the world.",
                    TotalDays = 2,
                    PeopleType = "Best for first-time visitors / city lovers",

                    ItineraryDays =
                    [
                        new ClientItineraryDayDto
                        {
                            DayNumber = 1,
                            DayTitle = "Old Town & City Energy",
                        },
                        new ClientItineraryDayDto
                        {
                            DayNumber = 2,
                            DayTitle = "Canal & History of Panama",
                        },
                    ],

                    ItineraryItems =
                    [
                        // DAY 1
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Arrive at Casco Viejo",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Walk cobblestone streets and explore plazas",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Visit Palacio de las Garzas",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Lunch at Mercado de Mariscos",
                            ItemOrderIndex = 3,
                            ItemType = "Location",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Walk along Cinta Costera",
                            ItemOrderIndex = 4,
                            ItemType = "Location",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Sunset overlooking Panama Bay",
                            ItemOrderIndex = 5,
                            ItemType = "Activity",
                            TimeOfDay = "Evening",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Rooftop bar in Casco Viejo",
                            ItemOrderIndex = 6,
                            ItemType = "Activity",
                            TimeOfDay = "Night",
                        },
                        // DAY 2
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Visit Panama Canal (Miraflores Locks)",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Watch ships pass through the locks",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Explore the Panama Canal Museum",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Lunch near the canal",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Visit Biomuseo",
                            ItemOrderIndex = 4,
                            ItemType = "Location",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Panoramic city and canal views",
                            ItemOrderIndex = 5,
                            ItemType = "Activity",
                            TimeOfDay = "Evening",
                        },
                    ],
                },
                new QueryWishlistDto
                {
                    WishlistId = Guid.NewGuid(),
                    WishlistHeroImage =
                        "https://wangq4yhmf94epv8.public.blob.vercel-storage.com/bocas.jpeg",
                    WishlistDescription =
                        "Caribbean rhythm, boat parties, and world-class waves — Panama’s wild side.",
                    WishlistName = "Bocas del Toro Party & Surf",
                    ShortStory =
                        "Spend four days hopping between islands, surfing tropical waves, and partying on boats with travelers from around the world.",
                    TotalDays = 4,
                    PeopleType = "Best for backpackers / party travelers / surfers",

                    ItineraryDays =
                    [
                        new ClientItineraryDayDto
                        {
                            DayNumber = 1,
                            DayTitle = "Arrival & Island Vibes",
                        },
                        new ClientItineraryDayDto
                        {
                            DayNumber = 2,
                            DayTitle = "Filthy Friday Party",
                        },
                        new ClientItineraryDayDto { DayNumber = 3, DayTitle = "Beaches & Surf" },
                        new ClientItineraryDayDto
                        {
                            DayNumber = 4,
                            DayTitle = "Snorkel & Departure",
                        },
                    ],

                    ItineraryItems =
                    [
                        // DAY 1
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Arrive in Bocas Town (Isla Colón)",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Check into hostel or hotel",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Explore town and waterfront",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Evening",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 1,
                            ItemDescription = "Dinner and drinks in Bocas Town",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Night",
                        },
                        // DAY 2
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Join Filthy Friday boat party",
                            ItemOrderIndex = 0,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Island hopping with drinks and music",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Swim stops in crystal clear water",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 2,
                            ItemDescription = "Return and recover in town",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Evening",
                        },
                        // DAY 3
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Morning surf at Red Frog Beach",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Explore jungle and find red frogs",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Visit Starfish Beach",
                            ItemOrderIndex = 2,
                            ItemType = "Location",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Relax in shallow water and hammocks",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 3,
                            ItemDescription = "Sunset at Wizard Beach",
                            ItemOrderIndex = 4,
                            ItemType = "Location",
                            TimeOfDay = "Evening",
                        },
                        // DAY 4
                        new ClientItineraryItemDto
                        {
                            DayNumber = 4,
                            ItemDescription = "Boat to Cayos Zapatilla",
                            ItemOrderIndex = 0,
                            ItemType = "Location",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 4,
                            ItemDescription = "Snorkel coral reefs and swim",
                            ItemOrderIndex = 1,
                            ItemType = "Activity",
                            TimeOfDay = "Morning",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 4,
                            ItemDescription = "Relax on the beach",
                            ItemOrderIndex = 2,
                            ItemType = "Activity",
                            TimeOfDay = "Noon",
                        },
                        new ClientItineraryItemDto
                        {
                            DayNumber = 4,
                            ItemDescription = "Return to Bocas Town and depart",
                            ItemOrderIndex = 3,
                            ItemType = "Activity",
                            TimeOfDay = "Afternoon",
                        },
                    ],
                },
            ];

            if (await context.Wishlists.AnyAsync())
                return;

            await wishlistRepo.CreateNewWishlist(wishlists[0], guidList["San Blas"]);
            await wishlistRepo.CreateNewWishlist(wishlists[1], guidList["Panama City"]);
            await wishlistRepo.CreateNewWishlist(wishlists[2], guidList["Bocas"]);
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
            var osakaCityIds = await context
                .Cities.Where(c => c.CityName == "Osaka")
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
                .Languages.Where(l => l.LanguageName == "English" || l.LanguageName == "Japanese")
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

            var destinationDto = new DestinationDto
            {
                DestinationName = "Kuromon Market",
                DestinationImage = "/images/destinations/kuromon_market.jpg",
                Description = new DescriptionJsonDto { },
                BestPeriodToVisit = new List<TravelPeriod>
                {
                    TravelPeriod.AprToJun,
                    TravelPeriod.OctToDec,
                },
                MaxCost = 30,
                MinCost = 15,
                SafetyLevel = 10,
                TimeZone = "Japan Standard Time",
                CountryId = japanCountryId,
                CityIds = osakaCityIds,
                LanguageIds = japanLangIds,
                CurrencyIds = japanCurrencyId,
                CategoryIds = kuromonCatIds,
                TagIds = kuromonTagIds,
            };

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

            var newDestinationList = destinationList
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
