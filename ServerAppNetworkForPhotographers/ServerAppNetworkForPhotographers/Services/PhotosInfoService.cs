using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotosInfoService
    {
        private readonly DataContext _context;

        public PhotosInfoService(DataContext context)
        {
            _context = context;
        }

        public async Task<PhotoInfo?> GetPhotoInfo(int photoId)
        {
            return await _context.PhotosInfo.FirstOrDefaultAsync(item => item.PhotoId == photoId);
        }

        public async Task<PhotoInfo> CreatePhotoInfo(CreatePhotoInfoDto photoInfoDto)
        {
            if (!await CheckExistencePhoto(photoInfoDto.PhotoId))
            {
                throw new NotFoundException(nameof(Photo), photoInfoDto.PhotoId);
            }

            if ((await GetPhotoInfo(photoInfoDto.PhotoId)) != null)
            {
                throw new UniqueModelException(photoInfoDto.PhotoId);
            }

            var photoInfo = new PhotoInfo(photoInfoDto);
            await _context.PhotosInfo.AddAsync(photoInfo);
            await _context.SaveChangesAsync();

            return photoInfo;
        }

        private async Task<bool> CheckExistencePhoto(int photoId)
        {
            return await _context.Photos.AnyAsync(item => item.Id == photoId);
        }
    }
}
