using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.Country;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hodrac_MVP_Backend.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly CustomDbContext _context;

        public CountryRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientCountryDto>> GetAllCountries()
        {
            var countries = await _context.Countries.ToListAsync();
            if (countries.IsNullOrEmpty())
                return null;
            return countries.Select(c => c.FromCountryToClientDto()).ToList();
        }
    }
}
