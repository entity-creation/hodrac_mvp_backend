using Hodrac_MVP_Backend.DTOs.Tag;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface ITagRepository
    {
        Task<List<ClientTagDto>> GetAllTags();
    }
}
