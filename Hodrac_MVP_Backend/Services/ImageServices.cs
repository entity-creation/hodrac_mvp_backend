namespace Hodrac_MVP_Backend.Services
{
    public class ImageServices
    {
        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var fileFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/destinations"
            );

            if (!File.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            string uniqueFileName =
                Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(fileFolder, uniqueFileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "images/destinations" + uniqueFileName;
        }
    }
}
