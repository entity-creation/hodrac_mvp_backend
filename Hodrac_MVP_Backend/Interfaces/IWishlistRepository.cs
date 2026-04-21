using Hodrac_MVP_Backend.DTOs.Wishlist;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface IWishlistRepository
    {
        Task CreateNewWishlist(QueryWishlistDto dto, List<Guid> destinationIds);
        Task<List<ClientWishlistDto>> GetAllWishlist();
        Task<ClientWishlistDto> GetWishlistById(Guid id);
    }
}
