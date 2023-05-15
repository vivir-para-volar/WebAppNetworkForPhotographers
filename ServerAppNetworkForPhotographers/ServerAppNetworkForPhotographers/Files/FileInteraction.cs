namespace ServerAppNetworkForPhotographers.Files
{
    public static class FileInteraction
    {
        private static string _profilePath = @$"{Environment.CurrentDirectory}\Files\ProfilePhotos\";
        private static string _blogMainPath = @$"{Environment.CurrentDirectory}\Files\BlogMainPhotos\";
        private static string _contentPath = @$"{Environment.CurrentDirectory}\Files\ContentPhotos\";

        static FileInteraction()
        {
            CreateDir(_profilePath);
            CreateDir(_blogMainPath);
            CreateDir(_contentPath);
        }

        public static async Task<string> GetBase64ProfilePhoto(string photoName)
        {
            return await GetBase64(_profilePath + photoName);
        }

        public static async Task<string> GetBase64BlogMainPhoto(string photoName)
        {
            return await GetBase64(_blogMainPath + photoName);
        }

        public static async Task<string> GetBase64ContentPhoto(int contentId, string photoName)
        {
            var path = _contentPath + contentId + "\\";
            return await GetBase64(path + photoName);
        }

        public static async Task<string> SaveProfilePhoto(IFormFile photo)
        {
            return await Save(_profilePath, photo);
        }

        public static async Task<string> SaveBlogMainPhoto(IFormFile photo)
        {
            return await Save(_blogMainPath, photo);
        }

        public static async Task<string> SaveContentPhoto(int contentId, IFormFile photo)
        {
            var path = _contentPath + contentId + "\\";
            CreateDir(path);

            return await SaveWithOwnName(path, photo);
        }

        public static void DeleteProfilePhoto(string photoName)
        {
            File.Delete(_profilePath + photoName);
        }

        public static void DeleteBlogMainPhoto(string photoName)
        {
            File.Delete(_blogMainPath + photoName);
        }

        public static void DeleteContentPhotos(int contentId)
        {
            var path = _contentPath + contentId;
            Directory.Delete(path, true);
        }

        private static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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

        private static async Task<string> SaveWithOwnName(string path, IFormFile photo)
        {
            string photoName = photo.FileName;
            path += photoName;

            using (var stream = File.Create(path))
            {
                await photo.CopyToAsync(stream);
            }

            return photoName;
        }

        private static async Task<string> GetBase64(string path)
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(path);
            string base64String = Convert.ToBase64String(fileBytes);

            return base64String;
        }
    }
}
