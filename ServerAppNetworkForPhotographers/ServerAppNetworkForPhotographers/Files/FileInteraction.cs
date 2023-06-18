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

        public static string GetProfilePhotoPath(string photoName)
        {
            return _profilePath + photoName;
        }

        public static string GetBlogMainPhotoPath(string photoName)
        {
            return _blogMainPath + photoName;
        }

        public static string GetContentPhotoPath(int contentId, string photoName)
        {
            return _contentPath + contentId + "\\" + photoName;
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

            return await Save(path, photo);
        }

        public static void DeleteProfilePhoto(string photoName)
        {
            var path = _profilePath + photoName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void DeleteBlogMainPhoto(string photoName)
        {
            var path = _blogMainPath + photoName;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void DeleteContentPhotos(int contentId)
        {
            var path = _contentPath + contentId;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
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
            string photoName = $"{Guid.NewGuid()}.jpeg";
            path += photoName;

            using (var stream = File.Create(path))
            {
                await photo.CopyToAsync(stream);
            }

            return photoName;
        }
    }
}
