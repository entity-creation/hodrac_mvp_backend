using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.Category;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hodrac_MVP_Backend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CustomDbContext _context;

        public CategoryRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientCategoryDto>?> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories.IsNullOrEmpty())
                return null;
            return categories.Select(c => c.FromCategoryToClientCategoryDto()).ToList();
        }
    }
}
