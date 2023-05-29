using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotosService
    {
        private readonly DataContext _context;

        public PhotosService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Photo>> CreatePhotos(int contentId, List<IFormFile> photos)
        {
            if (!await CheckExistenceContent(contentId))
            {
                throw new NotFoundException(nameof(Content), contentId);
            }

            var contentPhotos = new List<Photo>();

            foreach (var photo in photos)
            {
                var contentPhoto = await Photo.Save(contentId, photo);
                await _context.Photos.AddAsync(contentPhoto);

                contentPhotos.Add(contentPhoto);
            }
            await _context.SaveChangesAsync();

            return contentPhotos;
        }

        private async Task<bool> CheckExistenceContent(int contentId)
        {
            return await _context.Contents.AnyAsync(item => item.Id == contentId);
        }
    }
}
