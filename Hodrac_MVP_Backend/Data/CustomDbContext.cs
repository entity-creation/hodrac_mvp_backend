using Hodrac_MVP_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Hodrac_MVP_Backend.Data
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options) { }

        List<string> languageList =
        [
            "Afrikaans",
            "Albanian",
            "Amharic",
            "Arabic",
            "Armenian",
            "Assamese",
            "Azerbaijani",
            "Basque",
            "Belarusian",
            "Bengali",
            "Bosnian",
            "Bulgarian",
            "Burmese",
            "Catalan",
            "Cebuano",
            "Chichewa",
            "Chinese (Mandarin)",
            "Chinese (Cantonese)",
            "Corsican",
            "Croatian",
            "Czech",
            "Danish",
            "Dari",
            "Dutch",
            "English",
            "Esperanto",
            "Estonian",
            "Fijian",
            "Filipino",
            "Finnish",
            "French",
            "Frisian",
            "Galician",
            "Georgian",
            "German",
            "Greek",
            "Gujarati",
            "Haitian Creole",
            "Hausa",
            "Hawaiian",
            "Hebrew",
            "Hindi",
            "Hmong",
            "Hungarian",
            "Icelandic",
            "Igbo",
            "Indonesian",
            "Irish",
            "Italian",
            "Japanese",
            "Javanese",
            "Kannada",
            "Kazakh",
            "Khmer",
            "Kinyarwanda",
            "Korean",
            "Kurdish",
            "Kyrgyz",
            "Lao",
            "Latin",
            "Latvian",
            "Lithuanian",
            "Luxembourgish",
            "Macedonian",
            "Malagasy",
            "Malay",
            "Malayalam",
            "Maltese",
            "Maori",
            "Marathi",
            "Mongolian",
            "Nepali",
            "Norwegian",
            "Odia",
            "Pashto",
            "Persian",
            "Polish",
            "Portuguese",
            "Punjabi",
            "Romanian",
            "Russian",
            "Samoan",
            "Scottish Gaelic",
            "Serbian",
            "Sesotho",
            "Shona",
            "Sindhi",
            "Sinhala",
            "Slovak",
            "Slovenian",
            "Somali",
            "Spanish",
            "Sundanese",
            "Swahili",
            "Swedish",
            "Tajik",
            "Tamil",
            "Tatar",
            "Telugu",
            "Thai",
            "Turkish",
            "Turkmen",
            "Ukrainian",
            "Urdu",
            "Uyghur",
            "Uzbek",
            "Vietnamese",
            "Welsh",
            "Xhosa",
            "Yiddish",
            "Yoruba",
            "Zulu",
            "Dulegaya (Guna/Kuna)",
        ];

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<ItineraryDay> ItineraryDays { get; set; }
        public DbSet<ItineraryItem> ItineraryItems { get; set; }
        public DbSet<DestinationCategory> DestinationCategories { get; set; }
        public DbSet<DestinationCurrency> DestinationCurrencies { get; set; }
        public DbSet<DestinationLanguage> DestinationLanguages { get; set; }
        public DbSet<DestinationTag> DestinationTags { get; set; }
        public DbSet<DestinationCity> DestinationCities { get; set; }
        public DbSet<WishlistDestination> WishlistDestinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Destination>()
                .Property(d => d.BestPeriodToVisit)
                .HasConversion<string>();

            modelBuilder
                .Entity<Destination>()
                .HasOne(c => c.Country)
                .WithMany(d => d.Destinations)
                .HasForeignKey(d => d.CountryId);

            modelBuilder
                .Entity<ItineraryDay>()
                .HasOne(w => w.Wishlist)
                .WithMany(i => i.ItineraryDays)
                .HasForeignKey(w => w.WishlistId);

            modelBuilder
                .Entity<ItineraryItem>()
                .HasOne(i => i.ItineraryDay)
                .WithMany(i => i.ItineraryItems)
                .HasForeignKey(i => i.ItineraryDayId);

            modelBuilder
                .Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<DestinationCategory>(x =>
                x.HasKey(p => new { p.CategoryId, p.DestinationId })
            );

            modelBuilder
                .Entity<DestinationCategory>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.DestinationCategories)
                .HasForeignKey(d => d.DestinationId);
            modelBuilder
                .Entity<DestinationCategory>()
                .HasOne(x => x.Category)
                .WithMany(d => d.DestinationCategories)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<DestinationTag>(x =>
                x.HasKey(p => new { p.TagId, p.DestinationId })
            );

            modelBuilder
                .Entity<DestinationTag>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.DestinationTags)
                .HasForeignKey(d => d.DestinationId);

            modelBuilder
                .Entity<DestinationTag>()
                .HasOne(x => x.Tag)
                .WithMany(d => d.DestinationTags)
                .HasForeignKey(d => d.TagId);

            modelBuilder.Entity<DestinationCurrency>(x =>
                x.HasKey(p => new { p.CurrencyId, p.DestinationId })
            );

            modelBuilder
                .Entity<DestinationCurrency>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.DestinationCurrencies)
                .HasForeignKey(d => d.DestinationId);

            modelBuilder
                .Entity<DestinationCurrency>()
                .HasOne(x => x.Currency)
                .WithMany(d => d.DestinationCurrencies)
                .HasForeignKey(d => d.CurrencyId);

            modelBuilder.Entity<DestinationLanguage>(x =>
                x.HasKey(p => new { p.LanguageId, p.DestinationId })
            );

            modelBuilder
                .Entity<DestinationLanguage>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.DestinationLanguages)
                .HasForeignKey(d => d.DestinationId);

            modelBuilder
                .Entity<DestinationLanguage>()
                .HasOne(x => x.Language)
                .WithMany(d => d.DestinationLanguages)
                .HasForeignKey(d => d.LanguageId);

            modelBuilder.Entity<DestinationCity>(x =>
                x.HasKey(p => new { p.CityId, p.DestinationId })
            );

            modelBuilder
                .Entity<DestinationCity>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.DestinationCities)
                .HasForeignKey(d => d.DestinationId);

            modelBuilder
                .Entity<DestinationCity>()
                .HasOne(x => x.City)
                .WithMany(d => d.DestinationCities)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WishlistDestination>(x =>
                x.HasKey(p => new { p.DestinationId, p.WishlistId })
            );

            modelBuilder
                .Entity<WishlistDestination>()
                .HasOne(x => x.Wishlist)
                .WithMany(d => d.WishlistDestinations)
                .HasForeignKey(d => d.WishlistId);

            modelBuilder
                .Entity<WishlistDestination>()
                .HasOne(x => x.Destination)
                .WithMany(d => d.WishlistDestinations)
                .HasForeignKey(d => d.DestinationId);

            //modelBuilder
            //    .Entity<Language>()
            //    .HasData(
            //        languageList.Select(language =>
            //        {
            //            return new Language
            //            {
            //                LanguageId = Guid.NewGuid(),
            //                LanguageName = language,
            //            };
            //        })
            //    );
        }
    }
}
