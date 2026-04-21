using Hodrac_MVP_Backend.DTOs.Category;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<ClientCategoryDto>?> GetAllCategories();
    }
}
