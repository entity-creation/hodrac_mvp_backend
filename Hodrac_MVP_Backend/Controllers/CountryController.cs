using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/country")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepo;

        public CountryController(ICountryRepository countryRepo)
        {
            _countryRepo = countryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _countryRepo.GetAllCountries();
            if (response == null)
                return NotFound();
            return Ok(response);
        }
    }
}
