using Hodrac_MVP_Backend.DTOs.Destination;

namespace Hodrac_MVP_Backend.Interfaces
{
    public interface IDestinationRepository
    {
        Task CreateNewDestination(DestinationDto destinationDto);
        Task<List<ClientDestinationDto>> GetAllDestinations();
        Task<List<ClientDestinationDto>> GetDestinationByQuery(DestinationQueryDto query);
        Task<ClientDestinationDto?> GetDestinationByName(string name);
    }
}
