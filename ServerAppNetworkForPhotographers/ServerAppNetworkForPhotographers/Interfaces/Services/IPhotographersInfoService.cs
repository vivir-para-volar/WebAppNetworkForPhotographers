using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.PhotographersInfo;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IPhotographersInfoService
    {
        Task<PhotographerInfo?> GetPhotographerInfoByPhotographerId(int photographerId);
        Task<PhotographerInfo> UpdatePhotographerInfo(UpdatePhotographerInfoDto photographerInfoDto);
    }
}
