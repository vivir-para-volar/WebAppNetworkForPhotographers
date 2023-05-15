using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;

namespace ServerAppNetworkForPhotographers.Services
{
    public class PhotosInfoService : IPhotosInfoService
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

        public async Task<List<PhotoInfo>> CreatePhotosInfo(List<CreatePhotoInfoDto> photoInfoDtos)
        {
            foreach (var photoInfoDto in photoInfoDtos)
            {
                if (!await CheckExistencePhoto(photoInfoDto.PhotoId))
                {
                    throw new NotFoundException(nameof(Photo), photoInfoDto.PhotoId);
                }

                if ((await GetPhotoInfo(photoInfoDto.PhotoId)) != null)
                {
                    throw new UniqueModelException(photoInfoDto.PhotoId);
                }
            }

            var photosInfo = new List<PhotoInfo>();

            foreach (var photoInfoDto in photoInfoDtos)
            {
                var photoInfo = new PhotoInfo(photoInfoDto);
                await _context.PhotosInfo.AddAsync(photoInfo);

                photosInfo.Add(photoInfo);
            }
            await _context.SaveChangesAsync();

            return photosInfo;
        }

        private async Task<bool> CheckExistencePhoto(int photoId)
        {
            return await _context.Photos.AnyAsync(item => item.Id == photoId);
        }
    }
}
