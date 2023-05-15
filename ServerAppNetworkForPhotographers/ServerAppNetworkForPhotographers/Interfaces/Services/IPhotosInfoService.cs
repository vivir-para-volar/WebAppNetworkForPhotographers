using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotosInfoService
    {
        Task<PhotoInfo?> GetPhotoInfo(int photoId);
        Task<List<PhotoInfo>> CreatePhotosInfo(List<CreatePhotoInfoDto> photoInfoDtos);
    }
}
