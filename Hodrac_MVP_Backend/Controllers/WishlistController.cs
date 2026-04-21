using Hodrac_MVP_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hodrac_MVP_Backend.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository _wishlistRepo;

        public WishlistController(IWishlistRepository wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var wishlists = await _wishlistRepo.GetAllWishlist();
            return Ok(wishlists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var isValidGuid = Guid.TryParse(id, out var wishlistId);
            if (isValidGuid)
            {
                var wishlists = await _wishlistRepo.GetWishlistById(wishlistId);
                return Ok(wishlists);
            }
            return BadRequest();
        }
    }
}
