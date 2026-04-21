using Hodrac_MVP_Backend.Data;
using Hodrac_MVP_Backend.DTOs.Tag;
using Hodrac_MVP_Backend.Interfaces;
using Hodrac_MVP_Backend.Mappers.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Hodrac_MVP_Backend.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CustomDbContext _context;

        public TagRepository(CustomDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientTagDto>> GetAllTags()
        {
            var tags = await _context.Tags.ToListAsync();
            if (tags.IsNullOrEmpty())
                return null;
            return tags.Select(t => t.FromTagToClientDto()).ToList();
        }
    }
}
