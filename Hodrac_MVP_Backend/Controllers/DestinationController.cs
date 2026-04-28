using Hodrac_MVP_Backend.DTOs.Destination;
using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/destination")]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationRepository _destinationRepo;

        public DestinationController(IDestinationRepository destinationRepo)
        {
            _destinationRepo = destinationRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _destinationRepo.GetAllDestinations();
            if (response.Count == 0)
                return NotFound();
            return Ok(response);
        }

        [HttpGet]
        [Route("get-by-query")]
        public async Task<IActionResult> GetAllByQuery([FromQuery] DestinationQueryDto query)
        {
            var response = await _destinationRepo.GetDestinationByQuery(query);
            if (response.Count == 0)
                return NotFound();
            return Ok(response);
        }

        [HttpGet("get-by-id/{destinationId}")]
        public async Task<IActionResult> GetById([FromRoute] string destinationId)
        {
            var isValidId = Guid.TryParse(destinationId, out var id);
            if(!isValidId)
                return NotFound();
            var response = await _destinationRepo.GetDestinationById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
