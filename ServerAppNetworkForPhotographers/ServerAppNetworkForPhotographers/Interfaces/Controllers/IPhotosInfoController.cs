using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IPhotosInfoController
    {
        Task<ActionResult<PhotoInfo?>> GetPhotoInfo(int photoId);
        Task<ActionResult<List<PhotoInfo>>> CreatePhotosInfo(List<CreatePhotoInfoDto> photoInfoDtos);
    }
}
