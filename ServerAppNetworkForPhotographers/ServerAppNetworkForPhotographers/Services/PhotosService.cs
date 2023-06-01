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

        public async Task<Photo> Create(int contentId, IFormFile photo)
        {
            if (!await CheckExistenceContent(contentId))
            {
                throw new NotFoundException(nameof(Content), contentId);
            }

            var contentPhoto = await Photo.Save(contentId, photo);
            await _context.Photos.AddAsync(contentPhoto);
            await _context.SaveChangesAsync();

            return contentPhoto;
        }

        private async Task<bool> CheckExistenceContent(int contentId)
        {
            return await _context.Contents.AnyAsync(item => item.Id == contentId);
        }
    }
}
