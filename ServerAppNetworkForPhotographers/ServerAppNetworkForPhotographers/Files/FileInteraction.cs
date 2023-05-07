using System.IO;

namespace ServerAppNetworkForPhotographers.Files
{
    public static class FileInteraction
    {
        private static string _profilePath = @$"{Environment.CurrentDirectory}\Files\ProfilePhotos\";
        private static string _contentPath = @$"{Environment.CurrentDirectory}\Files\ContentPhotos\";

        static FileInteraction()
        {
            if (!Directory.Exists(_profilePath))
            {
                Directory.CreateDirectory(_profilePath);
            }

            if (!Directory.Exists(_contentPath))
            {
                Directory.CreateDirectory(_contentPath);
            }
        }

        public static async Task<string> GetBase64ProfilePhoto(string photoName)
        {
            string path = _profilePath + photoName;

            byte[] fileBytes = await File.ReadAllBytesAsync(path);
            string base64String = Convert.ToBase64String(fileBytes);

            return base64String;
        }

        public static async Task<string> SaveProfilePhoto(IFormFile photo)
        {
            return await Save(_profilePath, photo);
        }

        public static void DeleteProfilePhoto(string photoName)
        {
            string path = _profilePath + photoName;
            File.Delete(path);
        }

        private static async Task<string> Save(string path, IFormFile photo)
        {
            string photoName = $"{Guid.NewGuid()}.png";
            path += photoName;

            using (var stream = File.Create(path))
            {
                await photo.CopyToAsync(stream);
            }

            return photoName;
        }
    }
}
