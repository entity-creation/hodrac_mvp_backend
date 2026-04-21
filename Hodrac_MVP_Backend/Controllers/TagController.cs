using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepo;

        public TagController(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _tagRepo.GetAllTags();
            if (response == null)
                return NotFound();
            return Ok(response);
        }
    }
}
