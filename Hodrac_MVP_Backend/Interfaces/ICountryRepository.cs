using Hodrac_MVP_Backend.DTOs.Country;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface ICountryRepository
    {
        Task<List<ClientCountryDto>> GetAllCountries();
    }
}
