namespace Hodrac_MVP_Backend.DTOs.Destination
{
    public class DestinationImageDto
    {
        public string DestinationName { get; set; } = string.Empty;
        public required IFormFile ImageFile { get; set; }
    }
}
