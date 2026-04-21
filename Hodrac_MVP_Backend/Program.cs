using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowTrustedOrigins",
        policy =>
            policy
                .WithOrigins(
                    "http://localhost:5173",
                    "https://localhost:5173",
                    "https://www.hodrac.com"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
    );
});

builder.Services.AddDbContext<CustomDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MonsterConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }
    );
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IDestinationRepository, DestinationRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CustomDbContext>();
    await context.Database.MigrateAsync();
    await CustomDatabaseSeeder.SeedTagAsync(context);
    await CustomDatabaseSeeder.SeedCategoryAsync(context);
    await CustomDatabaseSeeder.SeedCurrencyAsync(context);
    await CustomDatabaseSeeder.SeedCountryAsync(context);
    await CustomDatabaseSeeder.SeedCityAsync(context);
    await CustomDatabaseSeeder.SeedDestination(context);
    await CustomDatabaseSeeder.SeedWishlist(context);
}

app.UseHttpsRedirection();

app.UseCors("AllowTrustedOrigins");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
