using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryRepo.GetAllCategories();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
